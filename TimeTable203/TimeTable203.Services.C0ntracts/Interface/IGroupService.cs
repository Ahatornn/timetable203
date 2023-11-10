using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Contracts.Interface
{
    public interface IGroupService
    {
        /// <summary>
        /// Получить список всех <see cref="GroupModel"/>
        /// </summary>
        Task<IEnumerable<GroupModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="GroupModel"/> по идентификатору
        /// </summary>
        Task<GroupModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую группу
        /// </summary>
        Task<GroupModel> AddAsync(Guid teacherId, string name, string description, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую группу
        /// </summary>
        Task<GroupModel> EditAsync(Guid teacherId, GroupModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую группу
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
