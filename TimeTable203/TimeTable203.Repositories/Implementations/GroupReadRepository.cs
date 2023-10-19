using TimeTable203.Context.Contracts;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Repositories.Anchors;

namespace TimeTable203.Repositories.Implementations
{
    public class GroupReadRepository : IGroupReadRepository, IReadRepositoryAnchor
    {
        private readonly ITimeTableContext context;

        public GroupReadRepository(ITimeTableContext context)
        {
            this.context = context;
        }

        Task<List<Group>> IGroupReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Groups.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Name)
                .ToList());

        Task<Group?> IGroupReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Groups.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, Group>> IGroupReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => Task.FromResult(context.Groups.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToDictionary(key => key.Id));
    }
}
