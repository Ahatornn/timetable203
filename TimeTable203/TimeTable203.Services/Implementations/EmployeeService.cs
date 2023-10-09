using AutoMapper;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Implementations
{
    public class EmployeeService : IEmployeeService, IServiceAnchor
    {
        private readonly IEmployeeReadRepository employeeReadRepository;
        private readonly IPersonReadRepository personReadRepository;
        private readonly IMapper mapper;

        public EmployeeService(IEmployeeReadRepository employeeReadRepository,
            IPersonReadRepository personReadRepository,
            IMapper mapper)
        {
            this.employeeReadRepository = employeeReadRepository;
            this.personReadRepository = personReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<EmployeeModel>> IEmployeeService.GetAllAsync(CancellationToken cancellationToken)
        {
            var employees = await employeeReadRepository.GetAllAsync(cancellationToken);
            var persons = await personReadRepository.GetByIdsAsync(employees.Select(x => x.PersonId).Distinct(), cancellationToken);
            var result = new List<EmployeeModel>();
            foreach (var employee in employees)
            {
                persons.TryGetValue(employee.PersonId, out var person);
                var empl = mapper.Map<EmployeeModel>(employee);
                empl.Person = person != null
                    ? mapper.Map<PersonModel>(person)
                    : null;
                result.Add(empl);
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
            var person = await personReadRepository.GetByIdAsync(item.PersonId, cancellationToken);
            var employee = mapper.Map<EmployeeModel>(item);
            employee.Person = person != null
                ? mapper.Map<PersonModel>(person)
                : null;
            return employee;
        }
    }
}
