using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.Models.Enums;

namespace TimeTable203.Services.Implementations
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentReadRepository documentReadRepository;

        public DocumentService(IDocumentReadRepository documentReadRepository)
        {
            this.documentReadRepository = documentReadRepository;
        }

        async Task<IEnumerable<DocumentModel>> IDocumentService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await documentReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new DocumentModel
            {
                Id = x.Id,
                Number = x.Number,
                Series = x.Series,
                IssuedAt = x.IssuedAt,
                IssuedBy = x.IssuedBy,
                DocumentType = (DocumentTypesModel)x.DocumentType,
                PersonId = x.PersonId,
            });
        }

        async Task<DocumentModel?> IDocumentService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await documentReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return new DocumentModel
            {
                Id = item.Id,
                Number = item.Number,
                Series = item.Series,
                IssuedAt = item.IssuedAt,
                IssuedBy = item.IssuedBy,
                DocumentType = (DocumentTypesModel)item.DocumentType,
                PersonId = item.PersonId,
            };
        }
    }
}
