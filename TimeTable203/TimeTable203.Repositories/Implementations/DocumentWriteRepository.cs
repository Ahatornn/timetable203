using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Repositories.Implementations
{
    /// <inheritdoc cref="IDocumentWriteRepository"/>
    public class DocumentWriteRepository : BaseWriteRepository<Document>,
        IDocumentWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DocumentWriteRepository"/>
        /// </summary>
        public DocumentWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
