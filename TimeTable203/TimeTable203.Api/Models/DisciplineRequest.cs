namespace TimeTable203.Api.Models
{
    /// <summary>
    /// Модель запроса на редактирование дисциплины
    /// </summary>
    public class DisciplineRequest : CreateDisciplineRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
