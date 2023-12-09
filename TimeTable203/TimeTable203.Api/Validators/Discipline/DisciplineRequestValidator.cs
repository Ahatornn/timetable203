using FluentValidation;
using TimeTable203.Api.ModelsRequest.Discipline;

namespace TimeTable203.Api.Validators.Discipline
{
    /// <summary>
    /// 
    /// </summary>
    public class DisciplineRequestValidator : AbstractValidator<DisciplineRequest>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public DisciplineRequestValidator()
        {
            RuleFor(discipline => discipline.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым или null");
        }
    }
}
