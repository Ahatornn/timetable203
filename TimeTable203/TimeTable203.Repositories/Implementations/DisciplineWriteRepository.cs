using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Repositories.Implementations
{
    /// <inheritdoc cref="IDisciplineWriteRepository"/>
    public class DisciplineWriteRepository : BaseWriteRepository<Discipline>,
        IDisciplineWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DisciplineWriteRepository"/>
        /// </summary>
        public DisciplineWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
