using Microsoft.EntityFrameworkCore;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Contracts
{
    /// <summary>
    /// Контекст работы с сущностями
    /// </summary>
    public interface ITimeTableContext
    {
        /// <summary>Список <inheritdoc cref="Discipline"/></summary>
        DbSet<Discipline> Disciplines { get; }

        /// <summary>Список <inheritdoc cref="Document"/></summary>
        DbSet<Document> Documents { get; }

        /// <summary>Список <inheritdoc cref="Employee"/></summary>
        DbSet<Employee> Employees { get; }

        /// <summary>Список <inheritdoc cref="Group"/></summary>
        DbSet<Group> Groups { get; }

        /// <summary>Список <inheritdoc cref="Person"/></summary>
        DbSet<Person> Persons { get; }

        /// <summary>Список <inheritdoc cref="TimeTableItem"/></summary>
        DbSet<TimeTableItem> TimeTableItems { get; }

    }
}
