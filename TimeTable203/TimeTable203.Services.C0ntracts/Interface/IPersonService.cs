using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.ModelsRequest;

namespace TimeTable203.Services.Contracts.Interface
{
    public interface IPersonService
    {
        /// <summary>
        /// Получить список всех <see cref="PersonModel"/>
        /// </summary>
        Task<IEnumerable<PersonModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="PersonModel"/> по идентификатору
        /// </summary>
        Task<PersonModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую персону
        /// </summary>
        Task<PersonModel> AddAsync(PersonRequestModel person, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую персону
        /// </summary>
        Task<PersonModel> EditAsync(PersonModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую персону
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
