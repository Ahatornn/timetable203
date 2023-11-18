using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Repositories.Implementations
{
    /// <inheritdoc cref="ITimeTableItemWriteRepository"/>
    public class TimeTableItemWriteRepository : BaseWriteRepository<TimeTableItem>,
        ITimeTableItemWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableItemWriteRepository"/>
        /// </summary>
        public TimeTableItemWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
