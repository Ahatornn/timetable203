using Microsoft.EntityFrameworkCore;
using Serilog;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Common.Entity.Repositories;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Repositories.Implementations
{
    public class DisciplineReadRepository : IDisciplineReadRepository, IRepositoryAnchor
    {
        private readonly IDbRead reader;

        public DisciplineReadRepository(IDbRead reader)
        {
            this.reader = reader;
            Log.Information("Инициализирован абстракция IDbReader в классе DisciplineReadRepository");
        }

        Task<IReadOnlyCollection<Discipline>> IDisciplineReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Discipline>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Discipline?> IDisciplineReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Discipline>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Discipline>> IDisciplineReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Discipline>()
            .NotDeletedAt()
            .ByIds(ids)
            .OrderBy(x => x.Name)
            .ToDictionaryAsync(key => key.Id, cancellation);
    }
}
