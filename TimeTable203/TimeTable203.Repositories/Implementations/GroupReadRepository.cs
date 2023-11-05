using Microsoft.EntityFrameworkCore;
using Serilog;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Common.Entity.Repositories;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Repositories.Implementations
{
    public class GroupReadRepository : IGroupReadRepository, IRepositoryAnchor
    {
        private readonly IDbRead reader;

        public GroupReadRepository(IDbRead reader)
        {
            this.reader = reader;
            Log.Information("Инициализирован абстракция IDbReader в классе GroupReadRepository");
        }

        Task<IReadOnlyCollection<Group>> IGroupReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Group>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Group?> IGroupReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Group>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Group>> IGroupReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Group>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(key => key.Id, cancellation);
    }
}
