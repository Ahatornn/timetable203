using AutoMapper;
using Serilog;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Services.Contracts.Exceptions;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.ModelsRequest;
using TimeTable203.Services.Helps;

namespace TimeTable203.Services.Implementations
{
    public class DocumentService : IDocumentService, IServiceAnchor
    {
        private readonly IDocumentReadRepository documentReadRepository;
        private readonly IDocumentWriteRepository documentWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPersonReadRepository personReadRepository;
        private readonly IMapper mapper;

        public DocumentService(IDocumentReadRepository documentReadRepository,
            IDocumentWriteRepository documentWriteRepository,
            IUnitOfWork unitOfWork,
            IPersonReadRepository personReadRepository,
            IMapper mapper)
        {
            this.documentReadRepository = documentReadRepository;
            this.personReadRepository = personReadRepository;
            this.documentWriteRepository = documentWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<DocumentModel>> IDocumentService.GetAllAsync(CancellationToken cancellationToken)
        {
            var documents = await documentReadRepository.GetAllAsync(cancellationToken);
            var persons = await personReadRepository.GetByIdsAsync(documents.Select(x => x.PersonId).Distinct(), cancellationToken);
            var result = new List<DocumentModel>();
            foreach (var document in documents)
            {
                cancellationToken.ThrowIfCancellationRequested();//Если пользователь ушел, выбрасываем исключение
                if (!persons.TryGetValue(document.PersonId, out var person))
                {
                    Log.Warning("Запрос вернул null(Person) IDocumentService.GetAllAsync");
                    continue;
                }
                var doc = mapper.Map<DocumentModel>(document);
                doc.Person = mapper.Map<PersonModel>(person);
                result.Add(doc);
            }
            return result;
        }

        async Task<DocumentModel?> IDocumentService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await documentReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                Log.Warning("Запрос вернул null IDocumentService.GetByIdAsync");
                return null;
            }

            var person = await personReadRepository.GetByIdAsync(item.PersonId, cancellationToken);

            var document = mapper.Map<DocumentModel>(item);
            document.Person = mapper.Map<PersonModel>(person);

            return document;
        }

        async Task<DocumentModel> IDocumentService.AddAsync(DocumentRequestModel document, CancellationToken cancellationToken)
        {
            var item = new Document
            {
                Id = Guid.NewGuid(),
                Number = document.Number,
                Series = document.Series,
                IssuedAt = document.IssuedAt,
                IssuedBy = document.IssuedBy,
                DocumentType = document.DocumentType,
            };

            var personValidate = new PersonHelpValidate(personReadRepository);
            var person = await personValidate.GetPersonByIdAsync(document.PersonId, cancellationToken);
            if (person != null)
            {
                item.PersonId = person.Id;
                item.Person = person;
            }

            documentWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<DocumentModel>(item);
        }

        async Task<DocumentModel> IDocumentService.EditAsync(DocumentRequestModel source, CancellationToken cancellationToken)
        {
            var targetDocument = await documentReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetDocument == null)
            {
                throw new TimeTableEntityNotFoundException<Document>(source.Id);
            }

            targetDocument.Number = source.Number;
            targetDocument.Series = source.Series;
            targetDocument.IssuedAt = source.IssuedAt;
            targetDocument.IssuedBy = source.IssuedBy;
            targetDocument.DocumentType = source.DocumentType;

            var personValidate = new PersonHelpValidate(personReadRepository);
            var person = await personValidate.GetPersonByIdAsync(source.PersonId, cancellationToken);
            if (person != null)
            {
                targetDocument.PersonId = person.Id;
                targetDocument.Person = person;
            }

            documentWriteRepository.Update(targetDocument);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<DocumentModel>(targetDocument);
        }

        async Task IDocumentService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetDocument = await documentReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetDocument == null)
            {
                throw new TimeTableEntityNotFoundException<Document>(id);
            }
            if (targetDocument.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Документ с идентификатором {id} уже удален");
            }

            documentWriteRepository.Delete(targetDocument);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
