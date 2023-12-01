using AutoMapper;
using Serilog;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Services.Contracts.Exceptions;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.ModelsRequest;

namespace TimeTable203.Services.Implementations
{
    public class EmployeeService : IEmployeeService, IServiceAnchor
    {
        private readonly IEmployeeReadRepository employeeReadRepository;
        private readonly IEmployeeWriteRepository employeeWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPersonReadRepository personReadRepository;
        private readonly IMapper mapper;

        public EmployeeService(IEmployeeReadRepository employeeReadRepository,
            IEmployeeWriteRepository employeeWriteRepository,
            IUnitOfWork unitOfWork,
            IPersonReadRepository personReadRepository,
            IMapper mapper)
        {
            this.employeeReadRepository = employeeReadRepository;
            this.employeeWriteRepository = employeeWriteRepository;
            this.unitOfWork = unitOfWork;
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

        async Task<EmployeeModel> IEmployeeService.AddAsync(EmployeeRequestModel employeeRequestModel, CancellationToken cancellationToken)
        {
            var item = new Employee
            {
                Id = Guid.NewGuid(),
                EmployeeType = employeeRequestModel.EmployeeType,
                PersonId = employeeRequestModel.PersonId,
            };

            employeeWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<EmployeeModel>(item);
        }
        async Task<EmployeeModel> IEmployeeService.EditAsync(EmployeeRequestModel source, CancellationToken cancellationToken)
        {
            var targetEmployee = await employeeReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetEmployee == null)
            {
                throw new TimeTableEntityNotFoundException<Employee>(source.Id);
            }

            targetEmployee.EmployeeType = source.EmployeeType;

            var person = await personReadRepository.GetByIdAsync(source.PersonId, cancellationToken);
            targetEmployee.PersonId = person!.Id;
            targetEmployee.Person = person;

            employeeWriteRepository.Update(targetEmployee);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<EmployeeModel>(targetEmployee);
        }
        async Task IEmployeeService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetEmployee = await employeeReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetEmployee == null)
            {
                throw new TimeTableEntityNotFoundException<Employee>(id);
            }
            if (targetEmployee.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Рабочий с идентификатором {id} уже удален");
            }

            employeeWriteRepository.Delete(targetEmployee);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
