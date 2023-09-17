namespace TimeTable203.Context.Contracts.Models
{
    /// <summary>
    /// Группа
    /// </summary>
    public class Group: BaseAuditEntity
    {
        /// <summary>
        /// Наименование группы
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание группы
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Студенты группы
        /// </summary>
        public ICollection<Person> Students { get; set; } = Array.Empty<Person>();

        /// <summary>
        /// Классный руководитель
        /// </summary>
        public Person? ClassroomTeacher { get; set; }
    }
}
