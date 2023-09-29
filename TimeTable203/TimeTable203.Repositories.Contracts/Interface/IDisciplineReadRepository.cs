using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Discipline"/>
    /// </summary>
    public interface IDisciplineReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Discipline"/>
        /// </summary>
        Task<List<Discipline>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Discipline"/> по идентификатору
        /// </summary>
        Task<Discipline?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
