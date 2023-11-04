namespace TimeTable203.Context.Contracts.Models
{
    /// <summary>
    /// Элемент расписания
    /// </summary>
    public class TimeTableItem : BaseAuditEntity
    {
        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTimeOffset EndDate { get; set; }

        /// <summary>
        /// Идентификатор дисциплины
        /// </summary>
        public Guid DisciplineId { get; set; }

        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public Discipline? Discipline { get; set; }

        /// <summary>
        /// Идентификатор группы
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public Group? Group { get; set; }

        /// <summary>
        /// Номер аудитории
        /// </summary>
        public short RoomNumber { get; set; }

        /// <summary>
        /// Преподаватель
        /// </summary>
        public Guid? TeacherId { get; set; }

        /// <summary>
        /// Преподаватель
        /// </summary>
        public Person? Person { get; set; }

    }
}
