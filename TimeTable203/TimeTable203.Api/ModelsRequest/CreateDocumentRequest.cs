using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Api.ModelsRequest
{
    public class CreateDocumentRequest
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
    }
}
