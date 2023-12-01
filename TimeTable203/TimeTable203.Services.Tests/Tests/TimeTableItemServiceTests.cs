using AutoMapper;
using FluentAssertions;
using TimeTable203.Context.Tests;
using TimeTable203.Repositories.Implementations;
using TimeTable203.Services.Automappers;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Implementations;
using Xunit;

namespace TimeTable203.Services.Tests.Tests
{
    public class TimeTableItemServiceTests : TimeTableContextInMemory
    {
        private readonly ITimeTableItemService timeTableItemService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableItemServiceTests"/>
        /// </summary>

        public TimeTableItemServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            timeTableItemService = new TimeTableItemService(
                new TimeTableItemReadRepository(Reader),
                new TimeTableItemWriteRepository(WriterContext),
                new DisciplineReadRepository(Reader),
                new GroupReadRepository(Reader),
                new EmployeeReadRepository(Reader),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение расписания по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await timeTableItemService.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение расписания по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.TimeTableItem();
            await Context.TimeTableItems.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await timeTableItemService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.StartDate,
                    target.EndDate
                });
        }

    }
}

