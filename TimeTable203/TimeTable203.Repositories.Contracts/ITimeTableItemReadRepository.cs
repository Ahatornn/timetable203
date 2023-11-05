using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="TimeTableItem"/>
    /// </summary>
    public interface ITimeTableItemReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="TimeTableItem"/> входящих в период между <see langword="startDate"/> и <see langword="endDate"/> включительно
        /// </summary>
        Task<IReadOnlyCollection<TimeTableItem>> GetAllByDateAsync(DateTimeOffset startDate, DateTimeOffset endDate, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="TimeTableItem"/> по идентификатору
        /// </summary>
        Task<TimeTableItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }

}
