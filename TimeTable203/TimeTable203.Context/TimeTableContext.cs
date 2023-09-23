using TimeTable203.Context.Contracts;
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
        }

        IEnumerable<Discipline> ITimeTableContext.Disciplines => disciplines;

        IEnumerable<Document> ITimeTableContext.Documents => documents;

        IEnumerable<Employee> ITimeTableContext.Employees => employees;

        IEnumerable<Group> ITimeTableContext.Groups => groups;

        IEnumerable<Person> ITimeTableContext.Persons => persons;

        IEnumerable<TimeTableItem> ITimeTableContext.TimeTableItems => timeTableItems;
    }
}
