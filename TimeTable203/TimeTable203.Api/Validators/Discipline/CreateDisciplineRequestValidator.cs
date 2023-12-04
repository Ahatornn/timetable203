using FluentValidation;
using TimeTable203.Api.ModelsRequest.Discipline;

namespace TimeTable203.Api.Validators.Discipline
{
    /// <summary>
    /// Валидарот класса <see cref="CreateDisciplineRequest"/>
    /// </summary>
    public class CreateDisciplineRequestValidator : AbstractValidator<CreateDisciplineRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="CreateDisciplineRequestValidator"/>
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
