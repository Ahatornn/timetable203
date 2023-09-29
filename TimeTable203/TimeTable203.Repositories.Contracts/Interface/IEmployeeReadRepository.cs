using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Employee"/>
    /// </summary>
    public interface IEmployeeReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Employee"/>
        /// </summary>
        Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Employee"/> по идентификатору
        /// </summary>
        Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
