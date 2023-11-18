using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Contracts.ModelsRequest
{
    public class TimeTableItemRequestModel
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
        /// Номер аудитории
        /// </summary>
        public short RoomNumber { get; set; }

        /// <summary>
        /// <inheritdoc cref="DisciplineModel"/>
        /// </summary>
        public Guid Discipline { get; set; }

        /// <summary>
        /// <inheritdoc cref="GroupModel"/>
        /// </summary>
        public Guid Group { get; set; }

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public Guid Teacher { get; set; }
    }
}
