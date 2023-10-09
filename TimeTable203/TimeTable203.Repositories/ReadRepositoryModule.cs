using Microsoft.Extensions.DependencyInjection;
using TimeTable203.Common;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Repositories.Implementations;

namespace TimeTable203.Repositories
{
    public class ReadRepositoryModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AddScoped<IDisciplineReadRepository, DisciplineReadRepository>();
            service.AddScoped<IDocumentReadRepository, DocumentReadRepository>();
            service.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();
            service.AddScoped<IGroupReadRepository, GroupReadRepository>();
            service.AddScoped<IPersonReadRepository, PersonReadRepository>();
            service.AddScoped<ITimeTableItemReadRepository, TimeTableItemReadRepository>();  
        }
    }
    /// <summary>
    /// Интерфейсный маркер, для регистрации ReadRepository
    /// </summary>
    public interface IReadRepositoryAnchor { };
}
