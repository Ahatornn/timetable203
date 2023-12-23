using System.Reflection;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using TimeTable203.Api.Controllers;
using TimeTable203.Api.Tests.Infrastructures;
using Xunit;

namespace TimeTable203.Api.Tests
{
    /// <summary>
    /// Тесты зависимостей контроллеров
    /// </summary>
    public class DependenciesTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DependenciesTests"/>
        /// </summary>
        public DependenciesTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory.WithWebHostBuilder(builder => builder.ConfigureTestAppConfiguration());
        }

        /// <summary>
        /// Проверка резолва зависимостей
        /// </summary>
        [Theory]
        [MemberData(nameof(ApiControllerCore))]
        public void ControllerCoreShouldBeResolved(Type controller)
        {
            // Arrange
            using var scope = factory.Services.CreateScope();

            // Act
            var instance = scope.ServiceProvider.GetRequiredService(controller);

            // Assert
            instance.Should().NotBeNull();
        }

        /// <summary>
        /// Коллекция контроллеров по администрированию
        /// </summary>
        public static IEnumerable<object[]>? ApiControllerCore =>
            Assembly.GetAssembly(typeof(DisciplineController))
                ?.DefinedTypes
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                .Where(type => !type.IsAbstract)
                .Select(type => new[] { type });
    }
}
