using AutoMapper;
using TimeTable203.Services.Automappers;
using Xunit;

namespace TimeTable203.Services.Tests
{
    public class MapperTest
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void TestMap()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });

            config.AssertConfigurationIsValid();
        }
    }
}
