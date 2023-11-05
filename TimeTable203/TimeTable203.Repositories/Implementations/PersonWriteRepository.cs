using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Repositories.Implementations
{
    /// <inheritdoc cref="IPersonWriteRepository"/>
    public class PersonWriteRepository : BaseWriteRepository<Person>,
        IPersonWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PersonWriteRepository"/>
        /// </summary>
        public PersonWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
