namespace TimeTable203.Context.Contracts.Models
{
    /// <summary>
    /// Предмет
    /// </summary>
    public class Discipline
    {
        /// <summary>
        /// ИД
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
    }
}
