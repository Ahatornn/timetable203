using TimeTable203.Api.Models.Enums;

namespace TimeTable203.Api.Models
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

        /// <inheritdoc cref="EmployeeTypesResponse"/>
        public EmployeeTypesResponse EmployeeType { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Мобильный телефон
        /// </summary>
        public string MobilePhone { get; set; } = string.Empty;
    }
}
