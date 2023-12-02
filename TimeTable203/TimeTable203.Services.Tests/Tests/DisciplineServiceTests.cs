using AutoMapper;
using FluentAssertions;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Context.Tests;
using TimeTable203.Repositories.Implementations;
using TimeTable203.Services.Automappers;
using TimeTable203.Services.Contracts.Exceptions;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Implementations;
using Xunit;

namespace TimeTable203.Services.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IDisciplineService"/>
    /// </summary>
    public class DisciplineServiceTests : TimeTableContextInMemory
    {
        private readonly IDisciplineService disciplineService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DisciplineServiceTests"/>
        /// </summary>
        public DisciplineServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            disciplineService = new DisciplineService(
                new DisciplineReadRepository(Reader),
                new DisciplineWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper());
        }

        /// <summary>
        /// Получение дисциплины по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => disciplineService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<TimeTableEntityNotFoundException<Discipline>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение дисциплины по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Discipline();
            await Context.Disciplines.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await disciplineService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Description,
                });
        }
    }
}
