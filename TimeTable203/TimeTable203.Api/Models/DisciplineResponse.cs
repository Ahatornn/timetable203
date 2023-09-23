namespace TimeTable203.Api.Models
{
    /// <summary>
    /// Модель ответа сущности дисциплин
    /// </summary>
    public class DisciplineResponse
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
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
