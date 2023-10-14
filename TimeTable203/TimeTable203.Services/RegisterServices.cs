using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TimeTable203.Services.Automappers;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Implementations;

namespace TimeTable203.Services
{
    public static class RegisterServices
    {
        /// <summary>
        /// 
        /// </summary>
        public static void AddMyServices(this IServiceCollection services)
        {
            services.AddScoped<IDisciplineService, DisciplineService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ITimeTableItemService, TimeTableItemService>();

            services.AddSingleton<Profile, ServiceProfile>();
        }
    }
}
