using Microsoft.EntityFrameworkCore;
using Serilog;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Common.Entity.Repositories;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Repositories.Implementations
{
    public class DocumentReadRepository : IDocumentReadRepository, IRepositoryAnchor
    {
        private readonly IDbRead reader;

        public DocumentReadRepository(IDbRead reader)
        {
            this.reader = reader;
            Log.Information("Инициализирован абстракция IDbReader в классе DocumentReadRepository");
        }

        Task<IReadOnlyCollection<Document>> IDocumentReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Document>()
                .NotDeletedAt()
                .OrderBy(x => x.Number)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Document?> IDocumentReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Document>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
