using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Contracts.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDisciplineService
    {
        /// <summary>
        /// Получить список всех <see cref="DisciplineModel"/>
        /// </summary>
        Task<IEnumerable<DisciplineModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="DisciplineModel"/> по идентификатору
        /// </summary>
        Task<DisciplineModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую дисциплину
        /// </summary>
        Task<DisciplineModel> AddAsync(string name, string description, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую дисциплину
        /// </summary>
        Task<DisciplineModel> EditAsync(DisciplineModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую дисциплину
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
