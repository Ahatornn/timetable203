using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Contracts.ModelsRequest
{
    public class EmployeeRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <inheritdoc cref="EmployeeTypes"/>
        public EmployeeTypes EmployeeType { get; set; }

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public Guid PersonId { get; set; }
    }
}
