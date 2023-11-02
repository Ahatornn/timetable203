using Microsoft.Extensions.DependencyInjection;
using TimeTable203.Common;
using TimeTable203.Context.Contracts;
using TimeTable203.Context.DB;

namespace TimeTable203.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AddScoped<ITimeTableContext, TimeTableContext>();
        }
    }
}
