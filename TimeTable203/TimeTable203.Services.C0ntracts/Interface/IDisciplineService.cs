using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Contracts.Interface
{
    public interface IDisciplineService
    {
        /// <summary>
        /// Получить список всех <see cref="DisciplineModel"/>
        /// </summary>
        Task<IEnumerable<DisciplineModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="DisciplineModel"/> по идентификатору
        /// </summary>
        Task<DisciplineModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
