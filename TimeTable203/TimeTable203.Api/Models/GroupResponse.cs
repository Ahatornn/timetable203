using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Api.Models
{
    /// <summary>
    /// Модель ответа сущности группы
    /// </summary>
    public class GroupResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
        public ICollection<PersonModel>? Students { get; set; }

        /// <summary>
        /// <inheritdoc cref="EmployeeModel"/>
        /// </summary>
        public PersonModel? ClassroomTeacher { get; set; }
    }
}
