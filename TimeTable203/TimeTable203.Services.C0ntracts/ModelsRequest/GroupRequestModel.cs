namespace TimeTable203.Services.Contracts.ModelsRequest
{
    public class GroupRequestModel
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
