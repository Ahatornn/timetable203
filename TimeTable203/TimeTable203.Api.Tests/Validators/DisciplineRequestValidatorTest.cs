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
        private readonly CreateDisciplineRequestValidator validatorCreateRequest;
        private readonly DisciplineRequestValidator validatorRequest;

        /// <summary>
        /// ctor
        /// </summary>
        public DisciplineRequestValidatorTest()
        {
            validatorRequest = new DisciplineRequestValidator();
            validatorCreateRequest = new CreateDisciplineRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new DisciplineRequest();

            // Act
            var result = validatorRequest.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorRequestShouldSuccess()
        {
            //Arrange
            var model = new DisciplineRequest
            {
                Name = $"Name{Guid.NewGuid():N}",
            };

            // Act
            var result = validatorRequest.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateDisciplineRequest();

            // Act
            var result = validatorCreateRequest.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = new CreateDisciplineRequest
            {
                Name = $"Name{Guid.NewGuid():N}",
            };

            // Act
            var result = validatorCreateRequest.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }
}
