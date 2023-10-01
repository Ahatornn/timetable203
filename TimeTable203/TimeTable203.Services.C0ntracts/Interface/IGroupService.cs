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
    }
}
