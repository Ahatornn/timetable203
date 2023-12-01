using FluentValidation;
using TimeTable203.Api.ModelsRequest.Person;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Api.Validators.Person
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePersonRequestValidator : AbstractValidator<CreatePersonRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public CreatePersonRequestValidator()
        {

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Фамилия не должна быть пустой или null");

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым или null");

            RuleFor(x => x.Email)
               .NotNull()
               .NotEmpty()
               .WithMessage("Почта не должна быть пустой или null")
               .EmailAddress()
               .WithMessage("Требуется действительная почта!");

            RuleFor(x => x.Phone)
             .NotNull()
             .NotEmpty()
             .WithMessage("Телефон не должна быть пустой или null");
        }
    }
}
