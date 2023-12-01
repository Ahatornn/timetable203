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
            RuleFor(discipline => discipline.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

            RuleFor(discipline => discipline.Number)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер не должен быть пустым или null");

            RuleFor(discipline => discipline.Series)
                .NotNull()
                .NotEmpty()
                .WithMessage("Серия не должна быть пустым или null");

            RuleFor(discipline => discipline.IssuedAt)
                .NotNull()
                .NotEmpty()
                .WithMessage("Дата выдачи не должна быть пустым или null");

            RuleFor(discipline => discipline.DocumentType)
                .NotNull()
                .WithMessage("Тип документа не должен быть null");

            RuleFor(discipline => discipline.PersonId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Персона не должна быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var person = await personReadRepository.GetByIdAsync(id, CancellationToken);
                    return person != null;
                })
                .WithMessage("Такой персоны не существует!");
        }
    }
}
