using TimeTable203.Context.Contracts.Enums;

namespace TimeTable203.Context.Contracts.Models
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Employee : BaseAuditEntity
    {
        /// <inheritdoc cref="EmployeeTypes"/>
        public EmployeeTypes EmployeeType { get; set; } = EmployeeTypes.Student;

        /// <summary>
        /// нужна для связи один ко многим по вторичному ключу <see cref="Group"/>
        /// </summary>
        public ICollection<Group>? Group { get; set; }

        /// <summary>
        /// Идентификатор <inheritdoc cref="Person"/>
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public Person? Person { get; set; }
    }
}
