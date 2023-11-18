using TimeTable203.Api.ModelsRequest.TimeTableItemRequest;

namespace TimeTable203.Api.ModelsRequest.TimeTableItem
{
    public class TimeTableItemRequest : CreateTimeTableItemRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
