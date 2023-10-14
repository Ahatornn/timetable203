using TimeTable203.Context;
using TimeTable203.Context.Contracts;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Repositories.Implementations;
using TimeTable203.Services;
using TimeTable203.Services.Automappers;

namespace TimeTable203.Api.Infrastructures
{
    public static class BuildExtensions
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IDisciplineReadRepository, DisciplineReadRepository>();
            services.AddScoped<IDocumentReadRepository, DocumentReadRepository>();
            services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();
            services.AddScoped<IGroupReadRepository, GroupReadRepository>();
            services.AddScoped<IPersonReadRepository, PersonReadRepository>();
            services.AddScoped<ITimeTableItemReadRepository, TimeTableItemReadRepository>();

            services.AddMyServices();

            services.AddScoped<ITimeTableContext, TimeTableContext>();

            services.AddAutoMapper(typeof(ServiceProfile));
        }
    }
}
