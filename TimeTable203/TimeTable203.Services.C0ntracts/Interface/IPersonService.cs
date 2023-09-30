using TimeTable203.Context.Contracts.Models;

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
    }
}
