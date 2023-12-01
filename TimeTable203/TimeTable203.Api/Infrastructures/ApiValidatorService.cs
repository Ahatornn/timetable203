using FluentValidation;
using TimeTable203.Api.ModelsRequest.Discipline;
using TimeTable203.Api.ModelsRequest.Document;
using TimeTable203.Api.ModelsRequest.Employee;
using TimeTable203.Api.ModelsRequest.Group;
using TimeTable203.Api.ModelsRequest.Person;
using TimeTable203.Api.Validators.Discipline;
using TimeTable203.Api.Validators.Document;
using TimeTable203.Api.Validators.Employee;
using TimeTable203.Api.Validators.Group;
using TimeTable203.Api.Validators.Person;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Services.Contracts.Exceptions;
using TimeTable203.Shared;

namespace TimeTable203.Api.Infrastructures
{
    internal sealed class ApiValidatorService : IApiValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ApiValidatorService(IPersonReadRepository personReadRepository,
            IEmployeeReadRepository employeeReadRepository)
        {
            validators.Add(typeof(CreateDisciplineRequest), new CreateDisciplineRequestValidator());
            validators.Add(typeof(DisciplineRequest), new DisciplineRequestValidator());

            validators.Add(typeof(CreateDocumentRequest), new CreateDocumentRequestValidator(personReadRepository));
            validators.Add(typeof(DocumentRequest), new DocumentRequestValidator(personReadRepository));

            validators.Add(typeof(CreateEmployeeRequest), new CreateEmployeeRequestValidator(personReadRepository));
            validators.Add(typeof(EmployeeRequest), new EmployeeRequestValidator(personReadRepository));

            validators.Add(typeof(CreateGroupRequest), new CreateGroupRequestValidator(employeeReadRepository));
            validators.Add(typeof(GroupRequest), new GroupRequestValidator(employeeReadRepository));

            validators.Add(typeof(CreatePersonRequest), new CreatePersonRequestValidator());
            validators.Add(typeof(PersonRequest), new PersonRequestValidator());
        }

        public async Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            var modelType = model.GetType();
            if (!validators.TryGetValue(modelType, out var validator))
            {
                throw new InvalidOperationException($"Не найден валидатор для модели {modelType}");
            }

            var context = new ValidationContext<TModel>(model);
            var result = await validator.ValidateAsync(context, cancellationToken);

            if (!result.IsValid)
            {
                throw new TimeTableValidationException(result.Errors.Select(x =>
                InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
