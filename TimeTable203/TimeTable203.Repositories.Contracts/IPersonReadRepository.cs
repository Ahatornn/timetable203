using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Repositories.Contracts
{

    /// <summary>
    /// Репозиторий чтения <see cref="Person"/>
    /// </summary>
    public interface IPersonReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Person"/>
        /// </summary>
        Task<IReadOnlyCollection<Person>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Person"/> по идентификатору
        /// </summary>
        Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Person"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Person>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Получить список всех <see cref="Person"/> по идентификатору группы
        /// </summary>
        Task<IReadOnlyCollection<Person>> GetAllByGroupIdAsync(Guid groupId, CancellationToken cancellationToken);
    }

}
