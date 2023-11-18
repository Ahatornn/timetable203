using AutoMapper;
using Serilog;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Implementations
{
    public class TimeTableItemService : ITimeTableItemService, IServiceAnchor
    {
        private readonly ITimeTableItemReadRepository timeTableItemReadRepository;
        private readonly IDisciplineReadRepository disciplineReadRepository;
        private readonly IGroupReadRepository groupReadRepository;
        private readonly IEmployeeReadRepository employeeReadRepository;
        private readonly IMapper mapper;

        public TimeTableItemService(ITimeTableItemReadRepository timeTableItemReadRepository,
            IDisciplineReadRepository disciplineReadRepository,
            IGroupReadRepository groupReadRepository,
            IEmployeeReadRepository employeeReadRepository,
            IMapper mapper)
        {
            this.timeTableItemReadRepository = timeTableItemReadRepository;
            this.disciplineReadRepository = disciplineReadRepository;
            this.groupReadRepository = groupReadRepository;
            this.employeeReadRepository = employeeReadRepository;
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
                cancellationToken.ThrowIfCancellationRequested();
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
    }
}
