using FluentValidation;
using TimeTable203.Api.ModelsRequest.TimeTableItemRequest;
using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Api.Validators.TimeTableItem
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateTimeTableItemRequestValidator : AbstractValidator<CreateTimeTableItemRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public CreateTimeTableItemRequestValidator(
            IEmployeeReadRepository employeeReadRepository,
            IDisciplineReadRepository disciplineReadRepository,
            IGroupReadRepository groupReadRepository)
        {

            RuleFor(x => x.StartDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("Начало занятия не должно быть пустым или null");

            RuleFor(x => x.EndDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("Конец занятия не должно быть пустым или null");

            RuleFor(x => x.RoomNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер кабинета не должен быть пустым или null");

            RuleFor(x => x.Discipline)
                .NotNull()
                .NotEmpty()
                .WithMessage("Дисциплина не должна быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var discipline = await disciplineReadRepository.GetByIdAsync(id, CancellationToken);
                    return discipline != null;
                })
                .WithMessage("Такой дисциплины не существует!");

            RuleFor(x => x.Group)
               .NotNull()
               .NotEmpty()
               .WithMessage("Группа не должна быть пустым или null")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var group = await groupReadRepository.GetByIdAsync(id, CancellationToken);
                   return group != null;
               })
               .WithMessage("Такой группы не существует!");

            RuleFor(x => x.Teacher)
               .NotNull()
               .NotEmpty()
               .WithMessage("Учитель не должен быть пустым или null")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var employee = await employeeReadRepository.GetByIdAsync(id, CancellationToken);
                   return employee != null;
               })
               .WithMessage("Такого учителя не существует!")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var employee = await employeeReadRepository.GetByIdAsync(id, CancellationToken);
                   if (employee == null)
                   {
                       return false;
                   }
                   return employee!.EmployeeType == EmployeeTypes.Teacher;
               })
                .WithMessage("Работник не соответствует категории: учитель!");
        }

    }

}
