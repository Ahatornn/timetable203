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
    public class EmployeeServiceTests : TimeTableContextInMemory
    {
        private readonly IEmployeeService employeeService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EmployeeServiceTests"/>
        /// </summary>

        public EmployeeServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            employeeService = new EmployeeService(
                new EmployeeReadRepository(Reader),
                new PersonReadRepository(Reader),
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение работника по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await employeeService.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение работника по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Employee();
            await Context.Employees.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await employeeService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.EmployeeType
                });
        }

    }
}

