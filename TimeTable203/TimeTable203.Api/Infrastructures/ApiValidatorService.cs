using FluentValidation;
using TimeTable203.Api.ModelsRequest.Discipline;
using TimeTable203.Api.Validators;
using TimeTable203.Services.Contracts.Exceptions;
using TimeTable203.Shared;

namespace TimeTable203.Api.Infrastructures
{
    internal sealed class ApiValidatorService : IApiValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ApiValidatorService()
        {
            validators.Add(typeof(DisciplineRequest), new DisciplineRequestValidator());
            validators.Add(typeof(CreateDisciplineRequest), new CreateDisciplineRequestValidator());
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
