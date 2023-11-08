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
        private readonly IDisciplineReadRepository disciplineReadRepository;

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

        public PersonHelpValidate(IDisciplineReadRepository disciplineReadRepository)
        {
            this.disciplineReadRepository = disciplineReadRepository;
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

        async public Task<Discipline?> GetDisciplineByIdAsync(Guid id_discipline, CancellationToken cancellationToken)
        {
            if (id_discipline != Guid.Empty)
            {
                var discipline = await disciplineReadRepository.GetByIdAsync(id_discipline, cancellationToken);
                if (discipline == null)
                {
                    throw new TimeTableInvalidOperationException("Такой дисциплины нет!");
                }
                return discipline;
            }
            return null;
        }
    }
}
