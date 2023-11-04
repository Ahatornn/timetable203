using AutoMapper;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Implementations
{
    public class GroupService : IGroupService, IServiceAnchor
    {
        private readonly IGroupReadRepository groupReadRepository;
        private readonly IPersonReadRepository personReadRepository;
        private readonly IEmployeeReadRepository employeeReadRepository;
        private readonly IMapper mapper;

        public GroupService(IGroupReadRepository groupReadRepository,
            IPersonReadRepository personReadRepository,
            IEmployeeReadRepository employeeReadRepository,
            IMapper mapper)
        {
            this.groupReadRepository = groupReadRepository;
            this.personReadRepository = personReadRepository;
            this.employeeReadRepository = employeeReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<GroupModel>> IGroupService.GetAllAsync(CancellationToken cancellationToken)
        {
            var groups = await groupReadRepository.GetAllAsync(cancellationToken);
            var employeeIds = groups.Where(x => x.EmployeeId.HasValue)
                .Select(x => x.EmployeeId!.Value)
                .Distinct();
            var teacherDictionary = await employeeReadRepository.GetPersonByEmployeeIdsAsync(employeeIds, cancellationToken);

            var listGroupModel = new List<GroupModel>();
            foreach (var group in groups)
            {
                var groupModel = mapper.Map<GroupModel>(group);
                groupModel.ClassroomTeacher = group.EmployeeId.HasValue &&
                                              teacherDictionary.TryGetValue(group.EmployeeId!.Value, out var teacher)
                    ? mapper.Map<PersonModel>(teacher)
                    : null;

                var students = await personReadRepository.GetAllByGroupIdAsync(group.Id, cancellationToken);
                groupModel.Students = mapper.Map<ICollection<PersonModel>>(students);

                listGroupModel.Add(groupModel);
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

            var groupModel = mapper.Map<GroupModel>(item);
            if (item.EmployeeId.HasValue)
            {
                var teacherDictionary = await employeeReadRepository.GetPersonByEmployeeIdsAsync(new[] { item.EmployeeId.Value }, cancellationToken);
                groupModel.ClassroomTeacher = teacherDictionary.TryGetValue(item.EmployeeId!.Value, out var teacher)
                    ? mapper.Map<PersonModel>(teacher)
                    : null;
            }

            var students = await personReadRepository.GetAllByGroupIdAsync(item.Id, cancellationToken);
            groupModel.Students = mapper.Map<ICollection<PersonModel>>(students);

            return groupModel;
        }
    }
}
