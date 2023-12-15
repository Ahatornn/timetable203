using AutoMapper;
using FluentAssertions;
using TimeTable203.Context.Tests;
using TimeTable203.Repositories.Implementations;
using TimeTable203.Services.Automappers;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Implementations;
using Xunit;

namespace TimeTable203.Services.Tests.Tests
{
    public class GroupServiceTests : TimeTableContextInMemory
    {
        private readonly IGroupService groupService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GroupServiceTests"/>
        /// </summary>

        public GroupServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            groupService = new GroupService(
                new GroupReadRepository(Reader),
                new GroupWriteRepository(WriterContext),
                UnitOfWork,
                new PersonReadRepository(Reader),
                new EmployeeReadRepository(Reader),
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение группы по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await groupService.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение группы по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Group();
            await Context.Groups.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await groupService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Description
                });
        }

        // <summary>
        /// Добавление группы, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.GroupRequestModel();

            //Act
            var act = await groupService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.Groups.Single(x =>
                x.Id == act.Id &&
                x.Name == target.Name
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Изменение группы, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Group();
            await Context.Groups.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.GroupRequestModel();
            targetModel.Id = target.Id;
            //Act
            var act = await groupService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Groups.Single(x =>
                x.Id == act.Id &&
                x.Name == targetModel.Name
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Удаление группы, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Group();
            await Context.Groups.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => groupService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Groups.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

    }
}

