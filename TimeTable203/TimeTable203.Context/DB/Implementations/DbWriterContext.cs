using TimeTable203.Common.Entity;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Common.Entity.InterfaceProvider;

namespace TimeTable203.Context.DB.Implementations
{
    public class DbWriterContext : IDbWriterContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DbWriterContext"/>
        /// </summary>
        public DbWriterContext(
            IDbWriter writer,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider)
        {
            Writer = writer;
            UnitOfWork = unitOfWork;
            DateTimeProvider = dateTimeProvider;
        }
        public IDbWriter Writer { get; }

        public IUnitOfWork UnitOfWork { get; }

        public IDateTimeProvider DateTimeProvider { get; }
    }
}
