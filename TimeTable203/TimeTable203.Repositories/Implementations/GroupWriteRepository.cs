using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Repositories.Implementations
{
    /// <inheritdoc cref="IGroupWriteRepository"/>
    public class GroupWriteRepository : BaseWriteRepository<Group>,
        IGroupWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GroupWriteRepository"/>
        /// </summary>
        public GroupWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
