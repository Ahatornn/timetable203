using Microsoft.EntityFrameworkCore;
using Serilog;
using TimeTable203.Common.Entity;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Anchors;
using TimeTable203.Repositories.Contracts.Interface;

namespace TimeTable203.Repositories.Implementations
{
    public class PersonReadRepository : IPersonReadRepository, IReadRepositoryAnchor
    {

        private readonly IDbRead reader;

        public PersonReadRepository(IDbRead reader)
        {
            this.reader = reader;
            Log.Information("Инициализирован абстракция IDbReader в классе PersonReadRepository");
        }

        Task<List<Person>> IPersonReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Person>()
                .NotDeletedAt()
                .OrderBy(x => x.LastName)
                .ToListAsync(cancellationToken);

        Task<Person?> IPersonReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Person>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Person>> IPersonReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Person>()
                .NotDeletedAt()
                .ByIds((IReadOnlyCollection<Guid>)ids)
                .OrderBy(x => x.LastName)
                .ToDictionaryAsync(key => key.Id, cancellation);
    }
}
