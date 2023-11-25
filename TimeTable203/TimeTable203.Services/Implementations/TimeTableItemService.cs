using AutoMapper;
using Serilog;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Services.Contracts.Exceptions;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.ModelsRequest;
using TimeTable203.Services.Helps;

namespace TimeTable203.Services.Implementations
{
    public class TimeTableItemService : ITimeTableItemService, IServiceAnchor
    {
        private readonly ITimeTableItemReadRepository timeTableItemReadRepository;
        private readonly ITimeTableItemWriteRepository timeTableItemWriteRepository;
        private readonly IDisciplineReadRepository disciplineReadRepository;
        private readonly IGroupReadRepository groupReadRepository;
        private readonly IEmployeeReadRepository employeeReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TimeTableItemService(ITimeTableItemReadRepository timeTableItemReadRepository,
            ITimeTableItemWriteRepository timeTableItemWriteRepository,
            IDisciplineReadRepository disciplineReadRepository,
            IGroupReadRepository groupReadRepository,
            IEmployeeReadRepository employeeReadRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.timeTableItemReadRepository = timeTableItemReadRepository;
            this.timeTableItemWriteRepository = timeTableItemWriteRepository;
            this.disciplineReadRepository = disciplineReadRepository;
            this.groupReadRepository = groupReadRepository;
            this.employeeReadRepository = employeeReadRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<TimeTableItemModel>> ITimeTableItemService.GetAllAsync(DateTimeOffset targetDate, CancellationToken cancellationToken)
        {
            var timeTableItems = await timeTableItemReadRepository.GetAllByDateAsync(targetDate.Date, targetDate.Date.AddDays(1).AddMilliseconds(-1), cancellationToken);

            var disciplineIds = timeTableItems.Select(x => x.DisciplineId).Distinct();
            var groupIds = timeTableItems.Select(x => x.GroupId).Distinct();
            var teacherIds = timeTableItems.Where(x => x.TeacherId.HasValue)
                .Select(x => x.TeacherId!.Value)
                .Distinct();

            var disciplineDictionary = await disciplineReadRepository.GetByIdsAsync(disciplineIds, cancellationToken);
            var groupDictionary = await groupReadRepository.GetByIdsAsync(groupIds, cancellationToken);
            var teacherDictionary = await employeeReadRepository.GetPersonByEmployeeIdsAsync(teacherIds, cancellationToken);

            var listTimeTableItemModel = new List<TimeTableItemModel>();
            foreach (var timeTableItem in timeTableItems)
            {
                if (!disciplineDictionary.TryGetValue(timeTableItem.DisciplineId, out var discipline))
                {
                    Log.Warning("Запрос вернул null(Discipline) ITimeTableItemService.GetAllAsync");
                    continue;
                }
                if (!groupDictionary.TryGetValue(timeTableItem.GroupId, out var group))
                {
                    Log.Warning("Запрос вернул null(Discipline) ITimeTableItemService.GetAllAsync");
                    continue;
                }
                if (timeTableItem.TeacherId == null ||
                    !teacherDictionary.TryGetValue(timeTableItem.TeacherId.Value, out var teacher))
                {
                    Log.Warning("Запрос вернул null(TeacherId) ITimeTableItemService.GetAllAsync");
                    continue;
                }
                var timeTable = mapper.Map<TimeTableItemModel>(timeTableItem);
                timeTable.Discipline = mapper.Map<DisciplineModel>(discipline);
                timeTable.Group = mapper.Map<GroupModel>(group);
                timeTable.Teacher = mapper.Map<PersonModel>(teacher);

                listTimeTableItemModel.Add(timeTable);
            }

            return listTimeTableItemModel;
        }

        async Task<TimeTableItemModel?> ITimeTableItemService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await timeTableItemReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            var discipline = await disciplineReadRepository.GetByIdAsync(item.DisciplineId, cancellationToken);
            var group = await groupReadRepository.GetByIdAsync(item.GroupId, cancellationToken);
            var timeTable = mapper.Map<TimeTableItemModel>(item);
            timeTable.Discipline = discipline != null
                ? mapper.Map<DisciplineModel>(discipline)
                : null;
            timeTable.Group = group != null
               ? mapper.Map<GroupModel>(group)
               : null;
            if (item.TeacherId != null)
            {
                var personDictionary = await employeeReadRepository.GetPersonByEmployeeIdsAsync(new[] { item.TeacherId.Value }, cancellationToken);
                timeTable.Teacher = personDictionary.TryGetValue(item.TeacherId.Value, out var teacher)
                  ? mapper.Map<PersonModel>(teacher)
                  : null;
            }
            return timeTable;
        }

        async Task<TimeTableItemModel> ITimeTableItemService.AddAsync(TimeTableItemRequestModel timeTable, CancellationToken cancellationToken)
        {
            var item = new TimeTableItem
            {
                Id = Guid.NewGuid(),
                StartDate = timeTable.StartDate,
                EndDate = timeTable.EndDate,
                RoomNumber = timeTable.RoomNumber
            };

            var employeeValidate = new PersonHelpValidate(employeeReadRepository);
            var employee = await employeeValidate.GetEmployeeByIdTeacherAsync(timeTable.Teacher, cancellationToken);
            if (employee != null)
            {
                item.TeacherId = employee.Id;
                item.Teacher = employee;
            }

            var groupValidate = new PersonHelpValidate(groupReadRepository);
            var group = await groupValidate.GetGroupByIdAsync(timeTable.Group, cancellationToken);
            if (group != null)
            {
                item.GroupId = group.Id;
                item.Group = group;
            }

            var disciplineValidate = new PersonHelpValidate(disciplineReadRepository);
            var discipline = await disciplineValidate.GetDisciplineByIdAsync(timeTable.Discipline, cancellationToken);
            if (discipline != null)
            {
                item.DisciplineId = discipline.Id;
                item.Discipline = discipline;
            }

            timeTableItemWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<TimeTableItemModel>(item);
        }

        async Task<TimeTableItemModel> ITimeTableItemService.EditAsync(TimeTableItemRequestModel source, CancellationToken cancellationToken)
        {
            var targetTimeTableItem = await timeTableItemReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetTimeTableItem == null)
            {
                throw new TimeTableEntityNotFoundException<TimeTableItem>(source.Id);
            }

            targetTimeTableItem.StartDate = source.StartDate;
            targetTimeTableItem.EndDate = source.EndDate;
            targetTimeTableItem.RoomNumber = source.RoomNumber;

            var employeeValidate = new PersonHelpValidate(employeeReadRepository);
            var employee = await employeeValidate.GetEmployeeByIdTeacherAsync(source.Teacher, cancellationToken);
            if (employee != null)
            {
                targetTimeTableItem.TeacherId = employee.Id;
                targetTimeTableItem.Teacher = employee;
            }

            var groupValidate = new PersonHelpValidate(groupReadRepository);
            var group = await groupValidate.GetGroupByIdAsync(source.Group, cancellationToken);
            if (group != null)
            {
                targetTimeTableItem.GroupId = group.Id;
                targetTimeTableItem.Group = group;
            }

            var disciplineValidate = new PersonHelpValidate(disciplineReadRepository);
            var discipline = await disciplineValidate.GetDisciplineByIdAsync(source.Discipline, cancellationToken);
            if (discipline != null)
            {
                targetTimeTableItem.DisciplineId = discipline.Id;
                targetTimeTableItem.Discipline = discipline;
            }

            timeTableItemWriteRepository.Update(targetTimeTableItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<TimeTableItemModel>(targetTimeTableItem);
        }

        async Task ITimeTableItemService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetTimeTableItem = await timeTableItemReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetTimeTableItem == null)
            {
                throw new TimeTableEntityNotFoundException<TimeTableItem>(id);
            }
            if (targetTimeTableItem.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Расписание с идентификатором {id} уже удален");
            }

            timeTableItemWriteRepository.Delete(targetTimeTableItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

    }
}
