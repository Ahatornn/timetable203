using TimeTable203.Common.Entity.InterfaceProvider;
using TimeTable203.Context.Anchors;

namespace TimeTable203.Context.DB.Implementations
{
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTimeOffset IDateTimeProvider.UtcNow => DateTimeOffset.UtcNow;
    }
}
