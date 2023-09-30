using TimeTable203.Context.Contracts;
using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context
{
    public class TimeTableContext: ITimeTableContext
    {
        private readonly IList<Discipline> disciplines;
        private readonly IList<Document> documents;
        private readonly IList<Employee> employees;
        private readonly IList<Group> groups;
        private readonly IList<Person> persons;
        private readonly IList<TimeTableItem> timeTableItems;

        public TimeTableContext()
        {
            disciplines = new List<Discipline>();
            documents = new List<Document>();
            employees = new List<Employee>();
            groups = new List<Group>();
            persons = new List<Person>();
            timeTableItems = new List<TimeTableItem>();
            Seed();
        }

        IEnumerable<Discipline> ITimeTableContext.Disciplines => disciplines;

        IEnumerable<Document> ITimeTableContext.Documents => documents;

        IEnumerable<Employee> ITimeTableContext.Employees => employees;

        IEnumerable<Group> ITimeTableContext.Groups => groups;

        IEnumerable<Person> ITimeTableContext.Persons => persons;

        IEnumerable<TimeTableItem> ITimeTableContext.TimeTableItems => timeTableItems;

        private void Seed()
        {
            var person1 = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "FirstName1",
                Phone = "Phone1",
                Patronymic = "Patronymic1",
                Email = "Email1",
                LastName = "LastName1",
            };
            var person2 = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "FirstName2",
                Phone = "Phone2",
                Patronymic = "Patronymic2",
                Email = "Email2",
                LastName = "LastName2",
            };

            persons.Add(person1);
            persons.Add(person2);

            var employee1 = new Employee
            {
                Id = Guid.NewGuid(),
                PersonId = person1.Id,
                EmployeeType = EmployeeTypes.Student,
            };

            var employee2 = new Employee
            {
                Id = Guid.NewGuid(),
                PersonId = person2.Id,
                EmployeeType = EmployeeTypes.Student,
            };

            employees.Add(employee1);
            employees.Add(employee2);
        }
    }
}
