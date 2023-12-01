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
    public class GroupReadRepositoryTests : TimeTableContextInMemory
    {
        private readonly IGroupReadRepository groupReadRepository;

        public GroupReadRepositoryTests()
        {
            groupReadRepository = new GroupReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список груп
        /// </summary>
        [Fact]
        public async Task GetAllGroupEmpty()
        {
            // Act
            var result = await groupReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список груп
        /// </summary>
        [Fact]
        public async Task GetAllGroupValue()
        {
            //Arrange
            var target = TestDataGenerator.Group();
            await Context.Groups.AddRangeAsync(target,
                TestDataGenerator.Group(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await groupReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение группы по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdGroupNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await groupReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение группы по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdGroupValue()
        {
            //Arrange
            var target = TestDataGenerator.Group();
            await Context.Groups.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await groupReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка груп по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsGroupsEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await groupReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка груп по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsGroupsValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Group();
            var target2 = TestDataGenerator.Group(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Group();
            var target4 = TestDataGenerator.Group();
            await Context.Groups.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await groupReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }
    }
}

