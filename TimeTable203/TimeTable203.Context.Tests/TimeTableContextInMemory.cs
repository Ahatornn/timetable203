using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TimeTable203.Common.Entity.InterfaceDB;

namespace TimeTable203.Context.Tests
{
    /// <summary>
    /// Класс <see cref="TimeTableContext"/> для тестов с базой в памяти. Один контекст на тест
    /// </summary>
    public abstract class TimeTableContextInMemory : IDisposable
    {
        /// <summary>
        /// Контекст <see cref="TimeTableContext"/>
        /// </summary>
        protected TimeTableContext Context { get; }

        /// <inheritdoc cref="IUnitOfWork"/>
        protected IUnitOfWork UnitOfWork => Context;

        /// <inheritdoc cref="IDbRead"/>
        protected IDbRead Reader => Context;

        protected IDbWriterContext WriterContext => new TestWriterContext(Context, UnitOfWork);

        protected TimeTableContextInMemory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TimeTableContext>()
                .UseInMemoryDatabase($"MoneronTests{Guid.NewGuid()}")
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            Context = new TimeTableContext(optionsBuilder.Options);
        }

        /// <inheritdoc cref="IDisposable"/>
        public void Dispose()
        {
            try
            {
                Context.Database.EnsureDeletedAsync().Wait();
                Context.Dispose();
            }
            catch (ObjectDisposedException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
    }
}
