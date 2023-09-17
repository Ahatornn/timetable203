namespace TimeTable203.Context.Contracts.Models
{
    /// <summary>
    /// Предмет
    /// </summary>
    public class Discipline : BaseAuditEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
