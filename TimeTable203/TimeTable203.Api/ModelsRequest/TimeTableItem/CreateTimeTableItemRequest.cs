using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Api.ModelsRequest.TimeTableItemRequest
{
    public class CreateTimeTableItemRequest
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
