using TimeTable203.Context;
using TimeTable203.Context.Contracts;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Repositories.Implementations;

namespace TimeTable203.Api.Infrastructures
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddDependences(this IServiceCollection service)
        {
            service.AddScoped<IDisciplineReadRepository, DisciplineReadRepository>();
            service.AddScoped<IDocumentReadRepository, DocumentReadRepository>();
            service.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();
            service.AddScoped<IGroupReadRepository, GroupReadRepository>();
            service.AddScoped<IPersonReadRepository, PersonReadRepository>();
            service.AddScoped<ITimeTableItemReadRepository, TimeTableItemReadRepository>();
            service.AddSingleton<ITimeTableContext, TimeTableContext>();
        }
    }
}
