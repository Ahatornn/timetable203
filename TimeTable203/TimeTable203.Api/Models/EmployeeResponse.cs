using TimeTable203.Context.Contracts.Enums;

namespace TimeTable203.Context.Contracts.Models
{
    /// <summary>
    /// Модель ответа сущности работников
    /// </summary>
    public class EmployeeResponse

    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <inheritdoc cref="EmployeeTypes"/>
        public EmployeeTypes EmployeeType { get; set; }

        /// <summary>
        /// Идентификатор <inheritdoc cref="PersonResponse"/>
        /// </summary>
        public Guid PersonId { get; set; }
    }
}
