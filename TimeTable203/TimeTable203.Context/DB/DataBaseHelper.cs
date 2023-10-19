using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TimeTable203.Context.DB
{
    public static class DataBaseHelper
    {
        public static DbContextOptions<TimeTableContext> Options()
        {
            var connectingString = GetConnectingString();
            var builderOption = new DbContextOptionsBuilder<TimeTableContext>();
            return builderOption
                    .UseSqlServer(connectingString)
                    .Options;
        }
        public static string GetConnectingString()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config.GetConnectionString("DefaultConnection");
        }
    }
}
