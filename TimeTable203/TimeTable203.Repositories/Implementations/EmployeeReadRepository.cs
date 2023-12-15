using Microsoft.EntityFrameworkCore;
using Serilog;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Common.Entity.Repositories;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Repositories.Implementations
{
    public class EmployeeReadRepository : IEmployeeReadRepository, IRepositoryAnchor
    {

        private readonly IDbRead reader;

        public EmployeeReadRepository(IDbRead reader)
        {
            this.reader = reader;
            Log.Information("Инициализирован абстракция IDbReader в классе EmployeeReadRepository");
        }

        Task<IReadOnlyCollection<Employee>> IEmployeeReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .OrderBy(x => x.EmployeeType)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Employee?> IEmployeeReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Employee>> IEmployeeReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .ByIds(ids)
                .ToDictionaryAsync(key => key.Id, cancellation);

        public Task<Dictionary<Guid, Person?>> GetPersonByEmployeeIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .ByIds(ids)
                .Select(x => new
                {
                    x.Id,
                    x.Person,
                })
                .ToDictionaryAsync(key => key.Id, val => val.Person, cancellation);
    }
}
