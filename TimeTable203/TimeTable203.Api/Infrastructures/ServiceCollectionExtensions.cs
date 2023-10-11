using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TimeTable203.Context;
using TimeTable203.Repositories;
using TimeTable203.Services;
using Microsoft.OpenApi.Models;
using AutoMapper;
using TimeTable203.Services.Automappers;

namespace TimeTable203.Api.Infrastructures
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddDependences(this IServiceCollection service)
        {
            //service.RegisterModule<ServiceModule>();
            //service.RegisterModule<ReadRepositoryModule>();
            //service.RegisterModule<ContextModule>();

            service.RegisterAssemblyInterfaceAssignableTo<IContextAnchor>(ServiceLifetime.Singleton);
            service.RegisterAssemblyInterfaceAssignableTo<IReadRepositoryAnchor>(ServiceLifetime.Scoped);
            service.RegisterAssemblyInterfaceAssignableTo<IServiceAnchor>(ServiceLifetime.Scoped);
        }
        public static void AddMapper(this IServiceCollection service)
        {
            var mapperConfig = new MapperConfiguration(ms =>
            {
                ms.AddProfile(new ServiceProfile());
            });
            mapperConfig.AssertConfigurationIsValid();
            var mapper = mapperConfig.CreateMapper();

            service.AddSingleton(mapper);
        }


        //public static void RegisterModule<TModule>(this IServiceCollection services) where TModule : Module
        //{
        //    var type = typeof(TModule);
        //    var instance = Activator.CreateInstance(type) as Module;
        //    if (instance == null)
        //    {
        //        return;
        //    }
        //    instance.CreateModule(services);
        //}

        public static void RegisterAssemblyInterfaceAssignableTo<TInterface>(this IServiceCollection services, ServiceLifetime lifetime)
        {
            var serviceType = typeof(TInterface);
            var types = serviceType.Assembly.GetTypes()
                .Where(x => serviceType.IsAssignableFrom(x) && !(x.IsAbstract || x.IsInterface));
            foreach (var type in types)
            {
                services.TryAdd(new ServiceDescriptor(type, type, lifetime));
                var interfaces = type.GetTypeInfo().ImplementedInterfaces
                .Where(i => i != typeof(IDisposable) && i.IsPublic && i != serviceType);
                foreach (var interfaceType in interfaces)
                {
                    services.TryAdd(new ServiceDescriptor(interfaceType,
                        provider => provider.GetRequiredService(type),
                        lifetime));
                }
            }
        }

        public static void GetSwaggerDocument(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Discipline", new OpenApiInfo { Title = "Сущность дисциплины", Version = "v1" });
                c.SwaggerDoc("Document", new OpenApiInfo { Title = "Сущность документы", Version = "v1" });
                c.SwaggerDoc("Employee", new OpenApiInfo { Title = "Сущность работники", Version = "v1" });
                c.SwaggerDoc("Group", new OpenApiInfo { Title = "Сущность группы", Version = "v1" });
                c.SwaggerDoc("Person", new OpenApiInfo { Title = "Сущность ученики", Version = "v1" });
                c.SwaggerDoc("TimeTableItem", new OpenApiInfo { Title = "Сущность элемент расписания", Version = "v1" });
            });
        }
        public static void GetSwaggerDocumentUI(this WebApplication app)
        {
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("Discipline/swagger.json", "Дисциплины");
                x.SwaggerEndpoint("Document/swagger.json", "Документы");
                x.SwaggerEndpoint("Employee/swagger.json", "Работники");
                x.SwaggerEndpoint("Group/swagger.json", "Группы");
                x.SwaggerEndpoint("Person/swagger.json", "Ученики");
                x.SwaggerEndpoint("TimeTableItem/swagger.json", "Элемент расписания");
            });
        }
    }
}
