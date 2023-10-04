using AutoMapper;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly IGroupReadRepository groupReadRepository;
        private readonly IEmployeeReadRepository employeeReadRepository;
        private readonly IMapper mapper;

        public GroupService(IGroupReadRepository groupReadRepository,
            IEmployeeReadRepository employeeReadRepository,
            IMapper mapper)
        {
            this.groupReadRepository = groupReadRepository;
            this.employeeReadRepository = employeeReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<GroupModel>> IGroupService.GetAllAsync(CancellationToken cancellationToken)
        {
            var groups = await groupReadRepository.GetAllAsync(cancellationToken);
            var groupId = groups.Select(x => x.EmployeeId).Distinct().Cast<Guid>();
            var employees = await employeeReadRepository.GetByIdsAsync(groupId, cancellationToken);
            var listGroupModel = new List<GroupModel>();
            foreach (var group in groups)
            {
                var employee = employees.FirstOrDefault(x => x.Id == group.EmployeeId);
                var groupEmployee = mapper.Map<GroupModel>(employee);
                var gr = mapper.Map<GroupModel>(group);
                gr.Employee = groupEmployee.Employee;
                listGroupModel.Add(gr);
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

            var employee = await employeeReadRepository.GetByIdAsync(item.EmployeeId ?? Guid.Empty, cancellationToken);
            var groupEmployee = mapper.Map<GroupModel>(employee);
            var gr = mapper.Map<GroupModel>(item);
            gr.Employee = groupEmployee.Employee;
            return gr;
        }
    }
}
