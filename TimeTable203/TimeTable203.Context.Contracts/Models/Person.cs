namespace TimeTable203.Context.Contracts.Models
{
    /// <summary>
    /// Сущность участника
    /// </summary>
    public class Person : BaseAuditEntity
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// сваязь один ко многим
        /// </summary>
        public Guid Group_id { get; set; }

        /// <summary>
        /// сваязь один ко многим
        /// </summary>
        public Group Group { get; set; }

        /// <summary>
        /// нужна для связи один ко многим по вторичному ключю <see cref="TimeTableItem"/>
        /// </summary>
        public ICollection<TimeTableItem> TimeTableItem { get; set; }

        /// <summary>
        /// нужна для связи один ко многим по вторичному ключю <see cref="Employee"/>
        /// </summary>
        public ICollection<Employee> Employee { get; set; }

        /// <summary>
        /// нужна для связи один ко многим по вторичному ключю <see cref="Document"/>
        /// </summary>
        public ICollection<Document> Document { get; set; }
    }
}
