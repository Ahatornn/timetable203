using TimeTable203.Common;
using TimeTable203.Context.Contracts;
using Microsoft.Extensions.DependencyInjection;
namespace TimeTable203.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AddSingleton<ITimeTableContext, TimeTableContext>();
        }
    }

    /// <summary>
    /// Интерфейсный маркер, для регистрации Context
    /// </summary>
    public interface IContextAnchor { };


}
