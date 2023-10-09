using TimeTable203.Context.Contracts.Enums;

namespace TimeTable203.Context.Contracts.Models
{
    public class Employee : BaseAuditEntity
    {
        /// <inheritdoc cref="EmployeeTypes"/>
        public EmployeeTypes EmployeeType { get; set; }

        /// <summary>
        /// Идентификатор <inheritdoc cref="Person"/>
        /// </summary>
        public Guid PersonId { get; set; }
    }
}
