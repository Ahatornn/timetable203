using Microsoft.EntityFrameworkCore;
using TimeTable203.Common.Entity;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Anchors;
using TimeTable203.Repositories.Contracts.Interface;

namespace TimeTable203.Repositories.Implementations
{
    public class DocumentReadRepository : IDocumentReadRepository, IReadRepositoryAnchor
    {
        private readonly IDbRead reader;

        public DocumentReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<List<Document>> IDocumentReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Document>()
                .NotDeletedAt()
                .OrderBy(x => x.Number)
                .ToListAsync(cancellationToken);

        Task<Document?> IDocumentReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Document>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
