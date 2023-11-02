using Microsoft.EntityFrameworkCore;
using Serilog;
using TimeTable203.Common.Entity;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Anchors;
using TimeTable203.Repositories.Contracts.Interface;

namespace TimeTable203.Repositories.Implementations
{
    public class GroupReadRepository : IGroupReadRepository, IReadRepositoryAnchor
    {
        private readonly IDbRead reader;

        public GroupReadRepository(IDbRead reader)
        {
            this.reader = reader;
            Log.Information("Инициализирован абстракция IDbReader в классе GroupReadRepository");
        }

        Task<List<Group>> IGroupReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Group>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

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
