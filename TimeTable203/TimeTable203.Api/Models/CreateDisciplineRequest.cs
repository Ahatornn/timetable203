namespace TimeTable203.Api.Models
{
    /// <summary>
    /// Модель запроса создания дисциплины
    /// </summary>
    public class CreateDisciplineRequest
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
