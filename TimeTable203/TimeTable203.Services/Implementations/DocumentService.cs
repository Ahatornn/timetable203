using AutoMapper;
using Serilog;
using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Services.Contracts.Exceptions;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.ModelsRequest;

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
        async public Task<Document> GetPersonByIdAsync(Guid id_person, Document item, CancellationToken cancellationToken)
        {
            if (id_person != Guid.Empty)
            {
                var targetPerson = await personReadRepository.GetByIdAsync(id_person, cancellationToken);
                if (targetPerson == null)
                {
                    throw new TimeTableEntityNotFoundException<Person>(id_person);
                }
                item.PersonId = id_person;
                item.Person = targetPerson;
            }
            return item;
        }
        async Task<IEnumerable<DocumentModel>> IDocumentService.GetAllAsync(CancellationToken cancellationToken)
        {
            var documents = await documentReadRepository.GetAllAsync(cancellationToken);
            var persons = await personReadRepository.GetByIdsAsync(documents.Select(x => x.PersonId).Distinct(), cancellationToken);
            var result = new List<DocumentModel>();
            foreach (var document in documents)
            {
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
            document.Person = person != null
                ? mapper.Map<PersonModel>(person)
                : null;

            return document;
        }

        async Task<DocumentModel> IDocumentService.AddAsync(Guid id_person, DocumentRequestModel document, CancellationToken cancellationToken)
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

            item = await GetPersonByIdAsync(id_person, item, cancellationToken);

            documentWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<DocumentModel>(item);
        }

        async Task<DocumentModel> IDocumentService.EditAsync(Guid id_person, DocumentModel source, CancellationToken cancellationToken)
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
            targetDocument.DocumentType = (DocumentTypes)source.DocumentType;

            targetDocument = await GetPersonByIdAsync(id_person, targetDocument, cancellationToken);

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
