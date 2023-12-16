using FluentAssertions;
using Newtonsoft.Json;
using TimeTable203.Api.Models;
using TimeTable203.Api.Tests.Infrastructures;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Contracts;
using TimeTable203.Context.Contracts.Models;
using Xunit;

namespace TimeTable203.Api.Tests
{
    [Collection(nameof(TimeTableApiTestCollection))]
    public class FirstIntergTests
    {
        private readonly CustomWebApplicationFactory factory;
        private readonly ITimeTableContext context;
        private readonly IUnitOfWork unitOfWork;

        public FirstIntergTests(TimeTableApiFixture fixture)
        {
            factory = fixture.Factory;
            context = fixture.Context;
            unitOfWork = fixture.UnitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task GetValue()
        {
            // Arrange
            var client = factory.CreateClient();
            var targetItem = new Discipline
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
                Description = $"NaDescriptionme{Guid.NewGuid():N}",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };
            await context.Disciplines.AddAsync(targetItem);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Discipline");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<DisciplineResponse>>(resultString);
            result.Should().NotBeNull()
                .And.ContainSingle(x => x.Id == targetItem.Id);
        }
    }
}
