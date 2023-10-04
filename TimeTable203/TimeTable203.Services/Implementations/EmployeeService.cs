using System;
using AutoMapper;
using TimeTable203.Context.Contracts.Models;
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
                var person = persons.FirstOrDefault(x => x.Id == employee.PersonId);
                //result.Add(new EmployeeModel
                //{
                //    Id = employee.Id,
                //    EmployeeType = (EmployeeTypesModel)employee.EmployeeType,
                //    Person = person == null
                //        ? null
                //        : new PersonModel
                //        {
                //            Id = person.Id,
                //            FirstName = person.FirstName,
                //            LastName = person.LastName,
                //            Patronymic = person.Patronymic,
                //            Email = person.Email,
                //            Phone = person.Phone,
                //        },
                //});
                var employeePerson = mapper.Map<EmployeeModel>(person);
                var empl = mapper.Map<EmployeeModel>(employee);
                empl.Person = employeePerson.Person;
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
            var employeePerson = mapper.Map<EmployeeModel>(person);
            var employee = mapper.Map<EmployeeModel>(item);
            employee.Person = employeePerson.Person;
            return employee;
        }
    }
}
