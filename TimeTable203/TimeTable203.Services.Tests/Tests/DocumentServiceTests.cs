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

    }
}
