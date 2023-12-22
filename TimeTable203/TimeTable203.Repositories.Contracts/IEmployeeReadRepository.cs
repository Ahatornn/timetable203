using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Context.Contracts.Models;
namespace TimeTable203.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Employee"/>
    /// </summary>
    public interface IEmployeeReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Employee"/>
        /// </summary>
        Task<IReadOnlyCollection<Employee>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Employee"/> по идентификатору
        /// </summary>
        Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Проверка есть ли <see cref="Employee"/> по указанному id
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Проверка <see cref="Employee"/> по id на категорию <see cref="EmployeeTypes.Teacher"/>
        /// </summary>
        Task<bool> AnyByIdWithTeacherAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Employee"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Employee>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Получить список <see cref="Person"/> по идентификаторам сотрудников
        /// </summary>
        Task<Dictionary<Guid, Person?>> GetPersonByEmployeeIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);
    }
}
