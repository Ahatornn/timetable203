using TimeTable203.Context.Contracts;
using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts.Interface;

namespace TimeTable203.Repositories.Implementations
{
    public class EmployeeReadRepository : IEmployeeReadRepository
    {
        private readonly ITimeTableContext context;

        public EmployeeReadRepository(ITimeTableContext context)
        {
            this.context = context;
        }

        Task<List<Employee>> IEmployeeReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Employees.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.EmployeeType)
                .ToList());

        Task<Employee?> IEmployeeReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Employees.FirstOrDefault(x => x.Id == id));

        Task<List<Employee>> IEmployeeReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => Task.FromResult(context.Employees.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Id)
                .ToList());

        Task<List<Employee>> IEmployeeReadRepository.GetByIdsWithTeacherAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => Task.FromResult(context.Employees.Where(x => x.DeletedAt == null && ids.Contains(x.Id) && x.EmployeeType == EmployeeTypes.Teacher)
                .OrderBy(x => x.Id)
                .ToList());
    }
}
