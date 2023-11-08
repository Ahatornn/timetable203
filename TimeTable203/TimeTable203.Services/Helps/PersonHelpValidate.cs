using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Services.Contracts.Exceptions;

namespace TimeTable203.Services.Helps
{
    public class PersonHelpValidate
    {
        private readonly IPersonReadRepository personReadRepository;
        private readonly IEmployeeReadRepository employeeReadRepository;
        private readonly IGroupReadRepository groupReadRepository;

        public PersonHelpValidate(IPersonReadRepository personReadRepository)
        {
            this.personReadRepository = personReadRepository;
        }

        public PersonHelpValidate(IEmployeeReadRepository employeeReadRepository)
        {
            this.employeeReadRepository = employeeReadRepository;
        }

        public PersonHelpValidate(IGroupReadRepository groupReadRepository)
        {
            this.groupReadRepository = groupReadRepository;
        }
        async public Task<Person?> GetPersonByIdAsync(Guid id_person, CancellationToken cancellationToken)
        {
            if (id_person != Guid.Empty)
            {
                var targetPerson = await personReadRepository.GetByIdAsync(id_person, cancellationToken);
                if (targetPerson == null)
                {
                    throw new TimeTableEntityNotFoundException<Person>(id_person);
                }
                return targetPerson;
            }
            return null;
        }

        async public Task<Employee?> GetEmployeeByIdTeacherAsync(Guid id_teacher, CancellationToken cancellationToken)
        {
            if (id_teacher != Guid.Empty)
            {
                var teacher = await employeeReadRepository.GetByIdAsync(id_teacher, cancellationToken);
                if (teacher == null)
                {
                    throw new TimeTableInvalidOperationException("Такого работника нет!");
                }
                if (teacher.EmployeeType != EmployeeTypes.Teacher)
                {
                    throw new TimeTableInvalidOperationException("Этот работник не является учителем!");
                }
                return teacher;
            }
            return null;
        }

        async public Task<Group?> GetGroupByIdAsync(Guid id_group, CancellationToken cancellationToken)
        {
            if (id_group != Guid.Empty)
            {
                var group = await groupReadRepository.GetByIdAsync(id_group, cancellationToken);
                if (group == null)
                {
                    throw new TimeTableInvalidOperationException("Такой группы нет!");
                }
                return group;
            }
            return null;
        }
    }
}
