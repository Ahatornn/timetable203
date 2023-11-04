using TimeTable203.Services.Contracts.Models.Enums;

namespace TimeTable203.Services.Contracts.Models
{
    /// <summary>
    /// Модель работников
    /// </summary>
    public class EmployeeModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <inheritdoc cref="EmployeeTypesModel"/>
        public EmployeeTypesModel EmployeeType { get; set; }

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public PersonModel? Person { get; set; }
    }
}
