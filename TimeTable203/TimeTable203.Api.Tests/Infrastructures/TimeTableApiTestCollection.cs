using Xunit;

namespace TimeTable203.Api.Tests.Infrastructures
{
    [CollectionDefinition(nameof(TimeTableApiTestCollection))]
    public class TimeTableApiTestCollection
        : ICollectionFixture<TimeTableApiFixture>
    {
    }
}
