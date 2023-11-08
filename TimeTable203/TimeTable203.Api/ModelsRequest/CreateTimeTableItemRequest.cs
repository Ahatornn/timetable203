namespace TimeTable203.Api.ModelsRequest
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
    }
}
