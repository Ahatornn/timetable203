using Microsoft.Extensions.DependencyInjection;
using TimeTable203.Common;
using TimeTable203.Context.Anchors;
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
