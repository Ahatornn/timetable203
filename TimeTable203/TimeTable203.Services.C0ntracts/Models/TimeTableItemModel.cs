using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Services.Contracts.Models
{
    /// <summary>
    /// Модель элемент расписания
    /// </summary>
    public class TimeTableItemModel
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
        /// <inheritdoc cref="DisciplineModel"/>
        /// </summary>
        public DisciplineModel? Discipline { get; set; }

        /// <summary>
        /// <inheritdoc cref="GroupModel"/>
        /// </summary>
        public GroupModel? Group { get; set; }

        /// <summary>
        /// Номер аудитории
        /// </summary>
        public short RoomNumber { get; set; }


        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public PersonModel? Teacher { get; set; }
    }
}
