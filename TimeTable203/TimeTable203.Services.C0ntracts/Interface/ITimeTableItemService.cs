using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.ModelsRequest;

namespace TimeTable203.Services.Contracts.Interface
{
    public interface ITimeTableItemService
    {
        /// <summary>
        /// Получить список всех <see cref="TimeTableItemModel"/>
        /// </summary>
        Task<IEnumerable<TimeTableItemModel>> GetAllAsync(DateTimeOffset targetDate, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="TimeTableItemModel"/> по идентификатору
        /// </summary>
        Task<TimeTableItemModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новое расписание
        /// </summary>
        Task<TimeTableItemModel> AddAsync(TimeTableItemRequestModel timeTable, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующее расписание
        /// </summary>
        Task<TimeTableItemModel> EditAsync(TimeTableItemModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующее расписание
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
