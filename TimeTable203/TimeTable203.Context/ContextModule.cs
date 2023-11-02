using Microsoft.Extensions.DependencyInjection;
using TimeTable203.Common;
using TimeTable203.Common.Entity;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Common.Entity.InterfaceProvider;
using TimeTable203.Context.Anchors;
using TimeTable203.Context.Contracts;
using TimeTable203.Context.DB;
using TimeTable203.Context.DB.Implementations;
using TimeTable203.Shared;

namespace TimeTable203.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IContextAnchor>(ServiceLifetime.Scoped);
        }
    }
}
