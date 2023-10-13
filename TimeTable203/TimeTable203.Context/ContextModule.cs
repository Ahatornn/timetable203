using Microsoft.Extensions.DependencyInjection;
using TimeTable203.Common;

namespace TimeTable203.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            //service.AddSingleton<ITimeTableContext, TimeTableContext>();
        }
    }

    /// <summary>
    /// Интерфейсный маркер, для регистрации Context
    /// </summary>
    public interface IContextAnchor { };


}
