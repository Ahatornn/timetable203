namespace TimeTable203.Context.Contracts.Models
{
    /// <summary>
    /// Группа
    /// </summary>
    public class Group : BaseAuditEntity
    {
        /// <summary>
        /// Наименование группы
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание группы
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Студенты группы связь один ко многим
        /// </summary>
        public ICollection<Person>? Students { get; set; }

        /// <summary>
        /// нужна для связи один ко многим
        /// </summary>
        public ICollection<TimeTableItem> TimeTableItem { get; set; }

        /// <summary>
        /// Классный руководитель
        /// </summary>
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public Employee? Employee { get; set; }
    }
}
