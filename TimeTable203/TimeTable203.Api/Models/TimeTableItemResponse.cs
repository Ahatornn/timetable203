namespace TimeTable203.Api.Models
{
    /// <summary>
    /// Модель ответа сущности элемент расписания
    /// </summary>
    public class TimeTableItemResponse
    {

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTimeOffset EndDate { get; set; }

        /// <summary>
        /// Наименование дисциплины
        /// </summary>
        public string NameDiscipline { get; set; } = string.Empty;

        /// <summary>
        /// Наименование группы
        /// </summary>
        public string NameGroup { get; set; } = string.Empty;

        /// <summary>
        /// Номер аудитории
        /// </summary>
        public short RoomNumber { get; set; }

        /// <summary>
        /// Преподаватель ФИО
        /// </summary>
        public string NamePerson { get; set; } = string.Empty;

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; } = string.Empty;
    }
}
