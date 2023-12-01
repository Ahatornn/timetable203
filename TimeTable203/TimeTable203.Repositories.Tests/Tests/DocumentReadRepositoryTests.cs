using FluentAssertions;
using TimeTable203.Context.Tests;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Repositories.Implementations;
using Xunit;

namespace TimeTable203.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IDocumentReadRepository"/>
    /// </summary>
    public class DocumentReadRepositoryTests : TimeTableContextInMemory
    {
        private readonly IDocumentReadRepository documentReadRepository;

        public DocumentReadRepositoryTests()
        {
            documentReadRepository = new DocumentReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список документов
        /// </summary>
        [Fact]
        public async Task GetAllDocumentEmpty()
        {
            // Act
            var result = await documentReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список документов
        /// </summary>
        [Fact]
        public async Task GetAllDocumentsValue()
        {
            //Arrange
            var target = TestDataGenerator.Document();
            await Context.Documents.AddRangeAsync(target,
                TestDataGenerator.Document(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await documentReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение документа по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdDocumentNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await documentReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение документа по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdDocumentValue()
        {
            //Arrange
            var target = TestDataGenerator.Document();
            await Context.Documents.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await documentReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }
    }
}
