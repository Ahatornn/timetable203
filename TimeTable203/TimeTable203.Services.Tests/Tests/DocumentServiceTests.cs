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
    public class DocumentServiceTests : TimeTableContextInMemory
    {
        private readonly IDocumentService documentService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DocumentServiceTests"/>
        /// </summary>

        public DocumentServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            documentService = new DocumentService(
                new DocumentReadRepository(Reader),
                new DocumentWriteRepository(WriterContext),
                UnitOfWork,
                new PersonReadRepository(Reader),
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение документа по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await documentService.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение документа по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Document();
            await Context.Documents.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await documentService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.DocumentType,
                    target.IssuedAt,
                    target.IssuedBy,
                });
        }

        /// <summary>
        /// Добавление документа, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.DocumentRequestModel();
            var person = TestDataGenerator.Person();
            await Context.Persons.AddAsync(person);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            target.PersonId = person.Id;
            var act = await documentService.AddAsync(target, CancellationToken);

            //Assert

            var entity = Context.Documents.Single(x =>
                x.Id == act.Id &&
                x.PersonId == target.PersonId
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Изменение документа, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var person = TestDataGenerator.Person();
            var personEdit = TestDataGenerator.Person();
            await Context.Persons.AddRangeAsync(person, personEdit);

            var target = TestDataGenerator.Document();
            target.PersonId = person.Id;
            await Context.Documents.AddAsync(target);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.DocumentRequestModel();
            targetModel.Id = target.Id;
            targetModel.PersonId = personEdit.Id;

            //Act
            var act = await documentService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Documents.Single(x =>
                x.Id == act.Id &&
                x.Number == targetModel.Number &&
                x.PersonId == targetModel.PersonId
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Удаление документа, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Document();
            await Context.Documents.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => documentService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Documents.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

    }
}
