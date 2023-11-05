using Microsoft.Extensions.DependencyInjection;
using TimeTable203.Common;
using TimeTable203.Shared;

namespace TimeTable203.Repositories
{
    public class RepositoryModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}
