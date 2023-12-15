using FluentValidation;
using TimeTable203.Api.ModelsRequest.TimeTableItemRequest;
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
                    var disciplineExists = await disciplineReadRepository.AnyByIdAsync(id, CancellationToken);
                    return disciplineExists;
                })
                .WithMessage("Такой дисциплины не существует!");

            RuleFor(x => x.Group)
               .NotNull()
               .NotEmpty()
               .WithMessage("Группа не должна быть пустым или null")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var groupExists = await groupReadRepository.AnyByIdAsync(id, CancellationToken);
                   return groupExists;
               })
               .WithMessage("Такой группы не существует!");

            RuleFor(x => x.Teacher)
               .NotNull()
               .NotEmpty()
               .WithMessage("Учитель не должен быть пустым или null")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var employeeExist = await employeeReadRepository.AnyByIdAsync(id, CancellationToken);
                   return employeeExist;
               })
               .WithMessage("Такого учителя не существует!")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var employeeExistsWithTeacher = await employeeReadRepository.AnyByIdWithTeacherAsync(id, CancellationToken);
                   return employeeExistsWithTeacher;
               })
                .WithMessage("Работник не соответствует категории: учитель!");
        }

    }

}
