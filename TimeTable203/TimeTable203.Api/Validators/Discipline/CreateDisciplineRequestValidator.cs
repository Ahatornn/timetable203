using FluentValidation;
using TimeTable203.Api.ModelsRequest.Discipline;

namespace TimeTable203.Api.Validators.Discipline
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDisciplineRequestValidator : AbstractValidator<CreateDisciplineRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public CreateDisciplineRequestValidator()
        {
            RuleFor(discipline => discipline.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым или null");
        }
    }
}
