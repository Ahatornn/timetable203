using Microsoft.Extensions.DependencyInjection;
using TimeTable203.Common;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Implementations;

namespace TimeTable203.Services
{
    public class ServiceModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AddScoped<IDisciplineService, DisciplineService>();
            service.AddScoped<IDocumentService, DocumentService>();
            service.AddScoped<IEmployeeService, EmployeeService>();
            service.AddScoped<IGroupService, GroupService>();
            service.AddScoped<IPersonService, PersonService>();
            service.AddScoped<ITimeTableItemService, TimeTableItemService>();
        }
    }
}
