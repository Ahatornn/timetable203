﻿using FluentValidation;
using TimeTable203.Api.ModelsRequest.Document;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Api.Validators.Document
{
    /// <summary>
    /// 
    /// </summary>
    public class DocumentRequestValidator : AbstractValidator<DocumentRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public DocumentRequestValidator(IPersonReadRepository personReadRepository)
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.Number)
               .NotNull()
               .NotEmpty()
               .WithMessage("Номер не должен быть пустым или null")
               .MaximumLength(8)
               .WithMessage("Номер больше 8 символов");

            RuleFor(x => x.Series)
                .NotNull()
                .NotEmpty()
                .WithMessage("Серия не должна быть пустым или null")
                .MaximumLength(12)
                .WithMessage("Серия больше 12 символов");

            RuleFor(x => x.IssuedAt)
                .NotNull()
                .NotEmpty()
                .WithMessage("Дата выдачи не должна быть пустым или null");

            RuleFor(x => x.DocumentType)
                .NotNull()
                .WithMessage("Тип документа не должен быть null");

            RuleFor(x => x.PersonId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Персона не должна быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var personExists = await personReadRepository.AnyByIdAsync(id, CancellationToken);
                    return personExists;
                })
                .WithMessage("Такой персоны не существует!");
        }
    }
}
