using FluentValidation.TestHelper;
using TimeTable203.Api.ModelsRequest.Discipline;
using TimeTable203.Api.Validators.Discipline;
using Xunit;

namespace TimeTable203.Api.Tests.Validators
{
    /// <summary>
    /// Тесты <see cref="DisciplineRequestValidator"/>
    /// </summary>
    public class DisciplineRequestValidatorTest
    {
        private readonly DisciplineRequestValidator validator;

        /// <summary>
        /// ctor
        /// </summary>
        public DisciplineRequestValidatorTest()
        {
            validator = new DisciplineRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = new DisciplineRequest();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldSuccess()
        {
            //Arrange
            var model = new DisciplineRequest
            {
                Name = $"Name{Guid.NewGuid():N}",
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }
}
