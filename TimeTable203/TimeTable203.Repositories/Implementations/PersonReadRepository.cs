using TimeTable203.Context.Contracts;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts.Interface;

namespace TimeTable203.Repositories.Implementations
{
    public class PersonReadRepository : IPersonReadRepository, IReadRepositoryAnchor
    {
        private readonly ITimeTableContext context;

        public PersonReadRepository(ITimeTableContext context)
        {
            this.context = context;
        }

        Task<List<Person>> IPersonReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Persons.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.LastName)
                .ToList());

        Task<Person?> IPersonReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Persons.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, Person>> IPersonReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => Task.FromResult(context.Persons.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.LastName)
                .ToDictionary(key => key.Id));
    }
}
