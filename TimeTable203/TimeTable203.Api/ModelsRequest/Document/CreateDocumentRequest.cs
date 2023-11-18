using TimeTable203.Api.Models.Enums;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Api.ModelsRequest.Document
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
        public DocumentTypesResponse DocumentType { get; set; } = DocumentTypesResponse.None;

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public Guid PersonId { get; set; }

    }
}
