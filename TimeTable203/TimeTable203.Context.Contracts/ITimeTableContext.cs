using Microsoft.EntityFrameworkCore;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Contracts
{
    /// <summary>
    /// Контекст работы с сущностями
    /// </summary>
    public interface ITimeTableContext
    {
        DbSet<Discipline> Disciplines { get; }

        DbSet<Document> Documents { get; }

        DbSet<Employee> Employees { get; }
        DbSet<Group> Groups { get; }

        DbSet<Person> Persons { get; }

        DbSet<TimeTableItem> TimeTableItems { get; }
    }
}
