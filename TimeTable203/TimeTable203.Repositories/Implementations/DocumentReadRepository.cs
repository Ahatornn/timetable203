using TimeTable203.Context.Contracts;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts.Interface;

namespace TimeTable203.Repositories.Implementations
{
    public class DocumentReadRepository : IDocumentReadRepository, IReadRepositoryAnchor
    {
        private readonly ITimeTableContext context;

        public DocumentReadRepository(ITimeTableContext context)
        {
            this.context = context;
        }

        Task<List<Document>> IDocumentReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Documents.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Number)
                .ToList());

        Task<Document?> IDocumentReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Documents.FirstOrDefault(x => x.Id == id));
    }
}
