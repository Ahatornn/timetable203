using Microsoft.EntityFrameworkCore;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context;
using TimeTable203.Context.Contracts;
using Xunit;

namespace TimeTable203.Api.Tests.Infrastructures
{
    public class TimeTableApiFixture : IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory factory;
        private TimeTableContext? timeTableContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableApiFixture"/>
        /// </summary>
        public TimeTableApiFixture()
        {
            factory = new CustomWebApplicationFactory();
        }

        Task IAsyncLifetime.InitializeAsync() => TimeTableContext.Database.MigrateAsync();

        async Task IAsyncLifetime.DisposeAsync()
        {
            await TimeTableContext.Database.EnsureDeletedAsync();
            await TimeTableContext.Database.CloseConnectionAsync();
            await TimeTableContext.DisposeAsync();
            await factory.DisposeAsync();
        }

        public CustomWebApplicationFactory Factory => factory;

        public ITimeTableContext Context => TimeTableContext;

        public IUnitOfWork UnitOfWork => TimeTableContext;

        internal TimeTableContext TimeTableContext
        {
            get
            {
                if (timeTableContext != null)
                {
                    return timeTableContext;
                }

                var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                timeTableContext = scope.ServiceProvider.GetRequiredService<TimeTableContext>();
                return timeTableContext;
            }
        }
    }
}
