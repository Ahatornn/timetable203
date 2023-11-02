using AutoMapper;
using Serilog;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Services.Anchors;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Implementations
{
    public class GroupService : IGroupService, IServiceAnchor
    {
        private readonly IGroupReadRepository groupReadRepository;
        private readonly IEmployeeReadRepository employeeReadRepository;
        private readonly IPersonReadRepository personReadRepository;
        private readonly IMapper mapper;

        public GroupService(IGroupReadRepository groupReadRepository,
            IEmployeeReadRepository employeeReadRepository,
            IPersonReadRepository personReadRepository,
            IMapper mapper)
        {
            this.groupReadRepository = groupReadRepository;
            this.employeeReadRepository = employeeReadRepository;
            this.personReadRepository = personReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<GroupModel>> IGroupService.GetAllAsync(CancellationToken cancellationToken)
        {
            var groups = await groupReadRepository.GetAllAsync(cancellationToken);
            var groupId = groups.Select(x => x.EmployeeId).Distinct().Cast<Guid>();
            var employees = await employeeReadRepository.GetByIdsAsync(groupId, cancellationToken);

            var listEmployees = new List<Employee>();
            foreach (var employee in employees.Values)
            {
                listEmployees.Add(employee);
            }
            var persons = await personReadRepository.GetByIdsAsync(listEmployees.Select(x => x.PersonId), cancellationToken);

            var listGroupModel = new List<GroupModel>();
            foreach (var group in groups)
            {
                var _group = mapper.Map<GroupModel>(group);
                if (!employees.TryGetValue(group.EmployeeId!.Value, out var employee))
                {
                    Log.Warning("Запрос вернул null(Employee) IEmployeeService.GetAllAsync");
                    continue;
                }
                if (!persons.TryGetValue(employee.PersonId, out var person))
                {
                    Log.Warning("Запрос вернул null(Person) IEmployeeService.GetAllAsync");
                    continue;
                }
                _group.Employee = mapper.Map<PersonModel>(person);
                listGroupModel.Add(_group);
            }
            return listGroupModel;
        }

        async Task<GroupModel?> IGroupService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await groupReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            var group = mapper.Map<GroupModel>(item);

            if (item.EmployeeId != null)
            {
                var employee = await employeeReadRepository.GetByIdAsync(item.EmployeeId!.Value, cancellationToken);
                var person = await personReadRepository.GetByIdAsync(employee.PersonId, cancellationToken);

                group.Employee = person != null
                    ? mapper.Map<PersonModel>(person)
                    : null;
            }
            return group;
        }
    }
}
