using FluentValidation;
using TimeTable203.Api.ModelsRequest.Group;
using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Api.Validators.Group
{
    /// <summary>
    /// 
    /// </summary>
    public class GroupRequestValidator : AbstractValidator<GroupRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public GroupRequestValidator(IEmployeeReadRepository employeeReadRepository)
        {
            RuleFor(x => x.Id)
              .NotNull()
              .NotEmpty()
              .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым или null");

            RuleFor(x => x.ClassroomTeacher)
                .NotNull()
                .NotEmpty()
                .WithMessage("Клаасный руководитель не должен быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var employee = await employeeReadRepository.GetByIdAsync(id!.Value, CancellationToken);
                    return employee != null;
                })
                .WithMessage("Такого работника не существует!")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var employee = await employeeReadRepository.GetByIdAsync(id!.Value, CancellationToken);
                    if (employee == null)
                    {
                        return false;
                    }
                    return employee!.EmployeeType == EmployeeTypes.Teacher;
                })
                 .WithMessage("Работник не соответствует категории учителя!");
        }
    }
}
