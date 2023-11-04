using AutoMapper;
using Serilog;
using TimeTable203.Repositories.Contracts;
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
                if (!persons.TryGetValue(employee.PersonId, out var person))
                {
                    Log.Warning("Запрос вернул null(Person) IEmployeeService.GetAllAsync");
                    continue;
                }
                var empl = mapper.Map<EmployeeModel>(employee);
                empl.Person = mapper.Map<PersonModel>(person);
                result.Add(empl);
            }

            return result;
        }

        async Task<EmployeeModel?> IEmployeeService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await employeeReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                Log.Warning("Запрос вернул null IEmployeeService.GetByIdAsync");
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
