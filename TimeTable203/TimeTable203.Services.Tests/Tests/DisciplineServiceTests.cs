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
        /// <summary>
        /// Добавление дисциплины, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Discipline();

            //Act
            var act = await disciplineService.AddAsync(target.Name, target.Description, CancellationToken);

            //Assert
            var entity = Context.Disciplines.Single(x =>
                x.Id == act.Id &&
                x.Name == target.Name
            );
            entity.Should().NotBeNull();
        }

        /// <summary>
        /// Изменение дисциплины, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Discipline();
            await Context.Disciplines.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.DisciplineModel();
            targetModel.Id = target.Id;

            //Act
            var act = await disciplineService.EditAsync(targetModel, CancellationToken);

            //Assert
            var entityTargetModel = Context.Disciplines.Single(x =>
                x.Id == act.Id &&
                x.Name == targetModel.Name
            );
            entityTargetModel.Should().NotBeNull();
        }

        /// <summary>
        /// Удаление дисциплины, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Discipline();
            await Context.Disciplines.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => disciplineService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Disciplines.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }
    }
}
