using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly IGroupReadRepository groupReadRepository;

        public GroupService(IGroupReadRepository groupReadRepository)
        {
            this.groupReadRepository = groupReadRepository;
        }

        async Task<IEnumerable<GroupModel>> IGroupService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await groupReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new GroupModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Students = x.Students,
                EmployeeId = x.EmployeeId,
            });
        }

        async Task<GroupModel?> IGroupService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await groupReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return new GroupModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Students = item.Students,
                EmployeeId = item.EmployeeId,
            };
        }
    }
}
