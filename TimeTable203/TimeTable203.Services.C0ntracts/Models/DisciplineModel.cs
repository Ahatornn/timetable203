namespace TimeTable203.Services.Contracts.Models
{
    /// <summary>
    /// Модель дисциплины
    /// </summary>
    public class DisciplineModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание дисциплины
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
