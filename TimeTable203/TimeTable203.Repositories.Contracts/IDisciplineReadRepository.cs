using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Discipline"/>
    /// </summary>
    public interface IDisciplineReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Discipline"/>
        /// </summary>
        Task<IReadOnlyCollection<Discipline>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Discipline"/> по идентификатору
        /// </summary>
        Task<Discipline?> GetByIdAsync(Guid id, CancellationToken cancellationToken);


        /// <summary>
        /// Проверка есть ли <see cref="Discipline"/> по указанному id
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Discipline"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Discipline>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);
    }
}
