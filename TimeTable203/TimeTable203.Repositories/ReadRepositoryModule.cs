using Microsoft.Extensions.DependencyInjection;
using TimeTable203.Common;
using TimeTable203.Repositories.Anchors;
using TimeTable203.Shared;

namespace TimeTable203.Repositories
{
    public class ReadRepositoryModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IReadRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}
