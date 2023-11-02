using System.Net;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
namespace TimeTable203.Logging
{
    public static class Debugger
    {
        public static void AddLoggerRegistr(this IServiceCollection services)
        {
            var version = Assembly.GetCallingAssembly().GetName().Version?.ToString();

            var host = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(host);
            IPAddress[] addr = ipEntry.AddressList;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/Log")
                .WriteTo.Seq("http://localhost:5050", apiKey: "tCDbflmP5IA8anbPkxEC")
                .Enrich.WithProperty("Version", version)
                .Enrich.WithProperty("IP", addr[4])
                .CreateLogger();

            services.AddLogging(log =>
            {
                log.AddSeq("http://localhost:5050", apiKey: "tCDbflmP5IA8anbPkxEC");
            });

            Log.Information("Регистрируем логирование");
        }
    }
}
