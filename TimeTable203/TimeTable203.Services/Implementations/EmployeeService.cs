using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Services.Contracts.Interface;

namespace TimeTable203.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeReadRepository employeeReadRepository;

        public EmployeeService(IEmployeeReadRepository employeeReadRepository)
        {
            this.employeeReadRepository = employeeReadRepository;
        }

        async Task<IEnumerable<EmployeeModel>> IEmployeeService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await employeeReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new EmployeeModel
            {
                Id = x.Id,
                EmployeeType = x.EmployeeType,
                PersonId = x.PersonId,
            });
        }

        async Task<EmployeeModel?> IEmployeeService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await employeeReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return new EmployeeModel
            {
                Id = item.Id,
                EmployeeType = item.EmployeeType,
                PersonId = item.PersonId,
            };
        }
    }
}
