using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Contracts
{
    /// <summary>
    /// Контекст работы с сущностями
    /// </summary>
    public interface ITimeTableContext
    {
        IEnumerable<Discipline> Disciplines { get; }

        IEnumerable<Document> Documents { get; }

        IEnumerable<Employee> Employees { get; }

        IEnumerable<Group> Groups { get; }

        IEnumerable<Person> Persons { get; }

        IEnumerable<TimeTableItem> TimeTableItems { get; }
    }
}
