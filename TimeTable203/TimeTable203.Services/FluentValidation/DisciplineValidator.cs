using FluentValidation;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Services.FluentValidation
{
    public class DisciplineValidator : AbstractValidator<Discipline>
    {
        public DisciplineValidator()
        {
            RuleFor(discipline => discipline.Name).NotNull().NotEmpty().WithMessage("Имя не должно быть пустым или null");
        }
    }
}
