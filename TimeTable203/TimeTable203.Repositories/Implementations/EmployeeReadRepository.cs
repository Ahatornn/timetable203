using Microsoft.EntityFrameworkCore;
using Serilog;
using TimeTable203.Common.Entity;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Anchors;
using TimeTable203.Repositories.Contracts.Interface;

namespace TimeTable203.Repositories.Implementations
{
    public class EmployeeReadRepository : IEmployeeReadRepository, IReadRepositoryAnchor
    {

        private readonly IDbRead reader;

        public EmployeeReadRepository(IDbRead reader)
        {
            this.reader = reader;
            Log.Information("Инициализирован абстракция IDbReader в классе EmployeeReadRepository");
        }

        Task<List<Employee>> IEmployeeReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .OrderBy(x => x.EmployeeType)
                .ToListAsync(cancellationToken);

        Task<Employee?> IEmployeeReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Employee>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Employee>> IEmployeeReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .ByIds((IReadOnlyCollection<Guid>)ids)
                .OrderBy(x => x.Id)
                .ToDictionaryAsync(key => key.Id, cancellation);
    }
}
