using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TimeTable203.Common;
using TimeTable203.Services.Anchors;
using TimeTable203.Services.Automappers;
using TimeTable203.Shared;

namespace TimeTable203.Services
{
    public class ServiceModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IServiceAnchor>(ServiceLifetime.Scoped);

            service.AddMapper<Profile>();
        }
    }
}
