using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace TimeTable203.Context.DB
{
    public static class DataBaseHelper
    {
        private static string connectingString = "";
        public static DbContextOptions<TimeTableApplicationContext> Options()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            connectingString = config.GetConnectionString("DefaultConnection");
            var builderOption = new DbContextOptionsBuilder<TimeTableApplicationContext>();
            return builderOption
                    .UseSqlServer(connectingString)
                    .Options;
        }
        public static string GetConnectingString() => connectingString;
    }
}
