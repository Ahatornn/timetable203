using Xunit;
using FluentAssertions.Extensions;
using FluentAssertions;

namespace TimeTable203.Api.Tests
{

    public class TestMapper
    {

        [Fact]
        public void TestValidate()
        {
            var item = 1.March(2022).At(20, 30).AsLocal();
            var item2 = 2.March(2022).At(20, 30).AsLocal();
            item.Should().NotBe(item2);
        }
    }
}
