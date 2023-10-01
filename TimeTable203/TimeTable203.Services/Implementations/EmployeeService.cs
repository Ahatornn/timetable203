using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.Models.Enums;

namespace TimeTable203.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeReadRepository employeeReadRepository;
        private readonly IPersonReadRepository personReadRepository;

        public EmployeeService(IEmployeeReadRepository employeeReadRepository,
            IPersonReadRepository personReadRepository)
        {
            this.employeeReadRepository = employeeReadRepository;
            this.personReadRepository = personReadRepository;
        }

        async Task<IEnumerable<EmployeeModel>> IEmployeeService.GetAllAsync(CancellationToken cancellationToken)
        {
            var employees = await employeeReadRepository.GetAllAsync(cancellationToken);
            var persons = await personReadRepository.GetByIdsAsync(employees.Select(x => x.PersonId).Distinct(), cancellationToken);
            var result = new List<EmployeeModel>();
            foreach (var employee in employees)
            {
                var person = persons.FirstOrDefault(x => x.Id == employee.PersonId);
                result.Add(new EmployeeModel
                {
                    Id = employee.Id,
                    EmployeeType = (EmployeeTypesModel)employee.EmployeeType,
                    Person = person == null
                        ? null
                        : new PersonModel
                        {
                            Id = person.Id,
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            Patronymic = person.Patronymic,
                            Email = person.Email,
                            Phone = person.Phone,
                        },
                });
            }

            return result;
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
                EmployeeType = (EmployeeTypesModel)item.EmployeeType,
            };
        }
    }
}
