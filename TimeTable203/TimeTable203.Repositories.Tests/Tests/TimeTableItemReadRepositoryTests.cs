using FluentAssertions;
using TimeTable203.Context.Tests;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Repositories.Implementations;
using Xunit;

namespace TimeTable203.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IDisciplineReadRepository"/>
    /// </summary>
    public class TimeTableItemReadRepositoryTests : TimeTableContextInMemory
    {
        private readonly ITimeTableItemReadRepository timeTableItemReadRepository;

        public TimeTableItemReadRepositoryTests()
        {
            timeTableItemReadRepository = new TimeTableItemReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список расписания
        /// </summary>
        [Fact]
        public async Task GetAllTimeTableItemEmpty()
        {

            //Arrange
            var date = DateTimeOffset.UtcNow.AddYears(3000);

            // Act
            var result = await timeTableItemReadRepository.GetAllByDateAsync(date, date, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список расписания
        /// </summary>
        [Fact]
        public async Task GetAllTimeTableItemsValue()
        {
            //Arrange
            var target = TestDataGenerator.TimeTableItem();
            await Context.TimeTableItems.AddRangeAsync(target,
                TestDataGenerator.TimeTableItem(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            var date = DateTimeOffset.UtcNow.AddHours(-10);

            // Act
            var result = await timeTableItemReadRepository.GetAllByDateAsync(date, date.AddDays(1), CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение расписания по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdTimeTableItemNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await timeTableItemReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение расписания по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdTimeTableItemValue()
        {
            //Arrange
            var target = TestDataGenerator.TimeTableItem();
            await Context.TimeTableItems.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await timeTableItemReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }


    }
}


