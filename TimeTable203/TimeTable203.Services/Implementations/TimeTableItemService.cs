using AutoMapper;
using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts.Interface;
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
            var employees = await employeeReadRepository.GetByIdsAsync(employeeId, cancellationToken);
            var listEmployees = new List<Employee>();
            foreach (var employee in employees.Values)
            {
                listEmployees.Add(employee);
            }
            var persons = await personReadRepository.GetByIdsAsync(listEmployees.Select(x => x.PersonId), cancellationToken);

            var listTimeTableItemModel = new List<TimeTableItemModel>();
            foreach (var timeTableItem in timeTableItems)
            {
                disciplines.TryGetValue(timeTableItem.DisciplineId, out var discipline);
                groups.TryGetValue(timeTableItem.GroupId, out var group);
                employees.TryGetValue(timeTableItem.Teacher ?? Guid.Empty, out var employee);
                if (employee == null)
                {
                    employee = new Employee();
                }
                persons.TryGetValue(employee.PersonId, out var person);

                var timeTable = mapper.Map<TimeTableItemModel>(timeTableItem);
                timeTable.Discipline = mapper.Map<DisciplineModel>(discipline);
                timeTable.Group = mapper.Map<GroupModel>(group);
                timeTable.Teacher = mapper.Map<PersonModel>(person);
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

            var person = await personReadRepository.GetByIdAsync(employee?.PersonId ?? Guid.Empty, cancellationToken);

            var timeTable = mapper.Map<TimeTableItemModel>(item);
            timeTable.Discipline = discipline != null
                ? mapper.Map<DisciplineModel>(discipline)
                : null;
            timeTable.Group = group != null
               ? mapper.Map<GroupModel>(group)
               : null;
            timeTable.Teacher = person != null
              ? mapper.Map<PersonModel>(person)
              : null;

            return timeTable;
        }
    }
}
