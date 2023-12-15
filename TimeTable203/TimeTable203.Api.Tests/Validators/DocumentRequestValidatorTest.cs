using System.Collections.Generic;
using FluentValidation.TestHelper;
using Moq;
using TimeTable203.Api.ModelsRequest.Document;
using TimeTable203.Api.Validators.Document;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Repositories.Implementations;
using Xunit;

namespace TimeTable203.Api.Tests.Validators
{
    public class DocumentRequestValidatorTest
    {
        private readonly CreateDocumentRequestValidator validationCreateRequest;
        private readonly DocumentRequestValidator validationRequest;

        private readonly Mock<IPersonReadRepository> personReadRepositoryMock;
        public DocumentRequestValidatorTest()
        {
            personReadRepositoryMock = new Mock<IPersonReadRepository>();
            validationCreateRequest = new CreateDocumentRequestValidator(personReadRepositoryMock.Object);
            validationRequest = new DocumentRequestValidator(personReadRepositoryMock.Object);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new DocumentRequest()
            {
                Id = Guid.NewGuid(),
                Number = "Number",
                Series = "Series",
                IssuedAt = DateTime.Now,
                PersonId = Guid.NewGuid(),
            };
            personReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.PersonId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            //Act
            var validation = await validationRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveValidationErrorFor(x => x.Id);
            validation.ShouldHaveValidationErrorFor(x => x.Series);
            validation.ShouldHaveValidationErrorFor(x => x.Number);
            validation.ShouldHaveValidationErrorFor(x => x.DocumentType);
            validation.ShouldHaveValidationErrorFor(x => x.PersonId);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldSuccess()
        {
            //Arrange
            var model = new DocumentRequest()
            {
                Id = Guid.NewGuid(),
                Number = "Number",
                Series = "Series",
                IssuedAt = DateTime.Now,
                PersonId = Guid.NewGuid(),
            };

            //Act
            var validation = await validationRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.Id);
            validation.ShouldNotHaveValidationErrorFor(x => x.Series);
            validation.ShouldNotHaveValidationErrorFor(x => x.Number);
            validation.ShouldNotHaveValidationErrorFor(x => x.DocumentType);
            validation.ShouldNotHaveValidationErrorFor(x => x.PersonId);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateDocumentRequest()
            {
                Number = "Number",
                Series = "Series",
                IssuedAt = DateTime.Now,
                PersonId = Guid.NewGuid(),
            };
            personReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.PersonId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Act
            var validation = await validationCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveValidationErrorFor(x => x.Series);
            validation.ShouldHaveValidationErrorFor(x => x.Number);
            validation.ShouldHaveValidationErrorFor(x => x.DocumentType);
            validation.ShouldHaveValidationErrorFor(x => x.PersonId);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = new CreateDocumentRequest()
            {
                Number = "Number",
                Series = "Series",
                IssuedAt = DateTime.Now,
                PersonId = Guid.NewGuid(),
            };

            //Act
            var validation = await validationCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.Series);
            validation.ShouldNotHaveValidationErrorFor(x => x.Number);
            validation.ShouldNotHaveValidationErrorFor(x => x.DocumentType);
            validation.ShouldNotHaveValidationErrorFor(x => x.PersonId);
        }
    }
}
