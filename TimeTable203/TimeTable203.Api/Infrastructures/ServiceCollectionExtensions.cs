using Microsoft.OpenApi.Models;
using TimeTable203.Context;
using TimeTable203.Repositories;
using TimeTable203.Services;

namespace TimeTable203.Api.Infrastructures
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddDependences(this IServiceCollection service)
        {
            service.RegisterModule<ServiceModule>();
            service.RegisterModule<ReadRepositoryModule>();
            service.RegisterModule<ContextModule>();
        }

        public static void RegisterModule<TModule>(this IServiceCollection services) where TModule : Common.Module
        {
            var type = typeof(TModule);
            var instance = Activator.CreateInstance(type) as Common.Module;
            if (instance == null)
            {
                return;
            }
            instance.CreateModule(services);
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
