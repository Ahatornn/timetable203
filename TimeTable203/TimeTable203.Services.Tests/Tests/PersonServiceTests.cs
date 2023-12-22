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
    public class PersonServiceTests : TimeTableContextInMemory
    {
        private readonly IPersonService personService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PersonServiceTests"/>
        /// </summary>

        public PersonServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            personService = new PersonService(
                new PersonReadRepository(Reader),
                new PersonWriteRepository(WriterContext),
                UnitOfWork,
                new GroupReadRepository(Reader),
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение персоны по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await personService.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение персоны по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Person();
            await Context.Persons.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await personService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.LastName,
                    target.FirstName,
                    target.Email,
                    target.Phone
                });
        }

        // <summary>
        /// Добавление персоны, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.PersonRequestModel();

            //Act
            var act = await personService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.Persons.Single(x =>
                x.Id == act.Id &&
                x.LastName == target.LastName
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Изменение персоны, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Person();
            await Context.Persons.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.PersonRequestModel();
            targetModel.Id = target.Id;
            targetModel.Patronymic = null;
            //Act
            var act = await personService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Persons.Single(x =>
                x.Id == act.Id &&
                x.LastName == targetModel.LastName &&
                x.Patronymic == null
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Удаление персоны, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Person();
            await Context.Persons.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => personService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Persons.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

    }
}

