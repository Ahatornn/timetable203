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
        /// Названия документа
        /// </summary>
        public string DocumentType { get; set; }

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
