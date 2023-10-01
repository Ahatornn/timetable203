using TimeTable203.Api.Models.Enums;
namespace TimeTable203.Api.Models
{
    /// <summary>
    /// Модель ответа сущности документов
    /// </summary>
    public class DocumentResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Серия документа
        /// </summary>
        public string Series { get; set; } = string.Empty;

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime IssuedAt { get; set; }

        /// <summary>
        /// Кем выдан
        /// </summary>
        public string IssuedBy { get; set; } = string.Empty;

        /// <summary>
        /// Тип документов
        /// </summary>
        public DocumentTypesResponse DocumentType { get; set; }

        /// <summary>
        /// Идентификатор <see cref="PersonResponse"/>
        /// </summary>
        public Guid PersonId { get; set; }
    }
}
