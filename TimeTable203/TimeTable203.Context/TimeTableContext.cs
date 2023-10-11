using Microsoft.EntityFrameworkCore;
using TimeTable203.Context.Contracts;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Context.DB;

namespace TimeTable203.Context
{
    public class TimeTableContext : ITimeTableContext, IContextAnchor
    {
        private readonly IList<Discipline> disciplines;
        private readonly IList<Document> documents;
        private readonly IList<Employee> employees;
        private readonly IList<Group> groups;
        private readonly IList<Person> persons;
        private readonly IList<TimeTableItem> timeTableItems;

        private readonly DbContextOptions<TimeTableApplicationContext> options;
        public TimeTableContext()
        {
            options = DataBaseHelper.Options();
            using (var db = new TimeTableApplicationContext(options))
            {
                disciplines = db.Disciplines.ToList();
                documents = db.Documents.ToList();
                employees = db.Employees.ToList();
                groups = db.Groups.ToList();
                persons = db.Persons.ToList();
                timeTableItems = db.TimeTableItems.ToList();
            }
        }
        IEnumerable<Discipline> ITimeTableContext.Disciplines => disciplines;

        IEnumerable<Document> ITimeTableContext.Documents => documents;

        IEnumerable<Employee> ITimeTableContext.Employees => employees;

        IEnumerable<Group> ITimeTableContext.Groups => groups;

        IEnumerable<Person> ITimeTableContext.Persons => persons;

        IEnumerable<TimeTableItem> ITimeTableContext.TimeTableItems => timeTableItems;

    }
}
