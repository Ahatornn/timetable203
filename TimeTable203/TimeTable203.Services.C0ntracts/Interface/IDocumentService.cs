using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.ModelsRequest;

namespace TimeTable203.Services.Contracts.Interface
{
    public interface IDocumentService
    {
        /// <summary>
        /// Получить список всех <see cref="DocumentModel"/>
        /// </summary>
        Task<IEnumerable<DocumentModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="DocumentModel"/> по идентификатору
        /// </summary>
        Task<DocumentModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый документ
        /// </summary>
        Task<DocumentModel> AddAsync(Guid id_person, DocumentRequestModel document, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий документ
        /// </summary>
        Task<DocumentModel> EditAsync(Guid id_person, DocumentModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий документ
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
