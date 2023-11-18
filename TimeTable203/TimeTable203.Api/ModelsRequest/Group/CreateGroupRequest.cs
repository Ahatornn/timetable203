using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Api.ModelsRequest.Group
{
    public class CreateGroupRequest
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public Guid? ClassroomTeacher { get; set; }
    }
}
