using TimeTable203.Common;
using TimeTable203.Common.Entity.InterfaceDB;

namespace TimeTable203.Api.Infrastructures
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
