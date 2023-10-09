using AutoMapper;
using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Implementations
{
    public class TimeTableItemService : ITimeTableItemService
    {
        private readonly ITimeTableItemReadRepository timeTableItemReadRepository;
        private readonly IDisciplineReadRepository disciplineReadRepository;
        private readonly IGroupReadRepository groupReadRepository;
        private readonly IEmployeeReadRepository employeeReadRepository;
        private readonly IPersonReadRepository personReadRepository;
        private readonly IMapper mapper;

        public TimeTableItemService(ITimeTableItemReadRepository timeTableItemReadRepository,
            IDisciplineReadRepository disciplineReadRepository,
            IGroupReadRepository groupReadRepository,
            IEmployeeReadRepository employeeReadRepository,
            IPersonReadRepository personReadRepository,
            IMapper mapper)
        {
            this.timeTableItemReadRepository = timeTableItemReadRepository;
            this.disciplineReadRepository = disciplineReadRepository;
            this.groupReadRepository = groupReadRepository;
            this.employeeReadRepository = employeeReadRepository;
            this.personReadRepository = personReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<TimeTableItemModel>> ITimeTableItemService.GetAllAsync(CancellationToken cancellationToken)
        {
            var timeTableItems = await timeTableItemReadRepository.GetAllAsync(cancellationToken);
            IEnumerable<Guid> disciplineId, groupId, employeeId;
            disciplineId = timeTableItems.Select(x => x.DisciplineId).Distinct().Cast<Guid>();
            groupId = timeTableItems.Select(x => x.GroupId).Distinct().Cast<Guid>();
            employeeId = timeTableItems.Select(x => x.Teacher).Distinct().Cast<Guid>();

            var disciplines = await disciplineReadRepository.GetByIdsAsync(disciplineId, cancellationToken);
            var groups = await groupReadRepository.GetByIdsAsync(groupId, cancellationToken);
            var employees = await employeeReadRepository.GetByIdsWithTeacherAsync(employeeId, cancellationToken);
            var persons = await personReadRepository.GetByIdsAsync(employees.Select(x => x.PersonId).Distinct(), cancellationToken);

            var listTimeTableItemModel = new List<TimeTableItemModel>();
            foreach (var timeTableItem in timeTableItems)
            {
                var discipline = disciplines.FirstOrDefault(x => x.Id == timeTableItem.DisciplineId);
                var group = groups.FirstOrDefault(x => x.Id == timeTableItem.GroupId);
                var person = persons.FirstOrDefault(x => x.Id == employees.FirstOrDefault(s => s.Id == timeTableItem.Teacher).PersonId);

                var timeTable = mapper.Map<TimeTableItemModel>(timeTableItem);
                var timeTableItemDiscipline = mapper.Map<TimeTableItemModel>(discipline);
                var timeTableItemGroup = mapper.Map<TimeTableItemModel>(group);
                var timeTableItemPerson = mapper.Map<TimeTableItemModel>(person);

                timeTable.Discipline = timeTableItemDiscipline.Discipline;
                timeTable.Group = timeTableItemGroup.Group;
                if (timeTableItemPerson != null)
                {
                    timeTable.Teacher = timeTableItemPerson.Teacher ?? new PersonModel() { Id = Guid.Empty };
                }


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
            var employee = await employeeReadRepository.GetByIdAsync(item.Teacher ?? Guid.Empty, cancellationToken);
            if (employee != null)
            {
                if (employee.EmployeeType != EmployeeTypes.Teacher)
                {
                    employee.PersonId = Guid.Empty;
                }
            }
            var person = await personReadRepository.GetByIdAsync(employee?.PersonId ?? Guid.Empty, cancellationToken);

            var timeTable = mapper.Map<TimeTableItemModel>(item);
            var timeTableItemDiscipline = mapper.Map<TimeTableItemModel>(discipline);
            var timeTableItemGroup = mapper.Map<TimeTableItemModel>(group);
            var timeTableItemPerson = mapper.Map<TimeTableItemModel>(person);

            timeTable.Discipline = timeTableItemDiscipline.Discipline;
            timeTable.Group = timeTableItemGroup.Group;
            timeTable.Teacher = timeTableItemPerson.Teacher ?? new PersonModel();
            return timeTable;
        }
    }
}
