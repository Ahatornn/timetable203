using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="TimeTableItem"/>
    /// </summary>
    public interface ITimeTableItemReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="TimeTableItem"/>
        /// </summary>
        Task<List<TimeTableItem>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="TimeTableItem"/> по идентификатору
        /// </summary>
        Task<TimeTableItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    }

}
