using TimeTable203.Context.Contracts.Enums;

namespace TimeTable203.Context.Contracts.Models
{
    /// <summary>
    /// Документы
    /// </summary>
    public class Document : BaseAuditEntity
    {
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
        public DateTime IssuedAt { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Кем выдан
        /// </summary>
        public string? IssuedBy { get; set; }

        /// <summary>
        /// Тип документов
        /// </summary>
        public DocumentTypes DocumentType { get; set; } = DocumentTypes.None;

        /// <summary>
        /// Идентификатор <see cref="Person"/>
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public Person Person { get; set; }
    }
}
