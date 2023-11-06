using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Repositories.Tests
{
    static internal class TestDataGenerator
    {
        static internal Discipline Discipline(Action<Discipline>? settings = null)
        {
            var result = new Discipline
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
                Description = $"Description{Guid.NewGuid():N}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            settings?.Invoke(result);
            return result;
        }
    }
}
