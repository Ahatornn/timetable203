using AutoMapper;
using TimeTable203.Api.Infrastructures;
using Xunit;

namespace TimeTable203.Api.Tests
{

    public class TestMapper
    {

        [Fact]
        public void TestValidate()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApiAutoMapperProfile());
            });

            config.AssertConfigurationIsValid();
        }
    }
}
