using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TimeTable203.Common;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Contracts;

namespace TimeTable203.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.TryAddScoped<ITimeTableContext>(provider => provider.GetRequiredService<TimeTableContext>());
            service.TryAddScoped<IDbRead>(provider => provider.GetRequiredService<TimeTableContext>());
            service.TryAddScoped<IDbWriter>(provider => provider.GetRequiredService<TimeTableContext>());
            service.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<TimeTableContext>());
        }
    }
}
