using System.Reflection.Metadata;
using System;
using AutoMapper;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.Models.Enums;

namespace TimeTable203.Services.Implementations
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentReadRepository documentReadRepository;
        private readonly IPersonReadRepository personReadRepository;
        private readonly IMapper mapper;

        public DocumentService(IDocumentReadRepository documentReadRepository,
            IPersonReadRepository personReadRepository,
            IMapper mapper)
        {
            this.documentReadRepository = documentReadRepository;
            this.personReadRepository = personReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<DocumentModel>> IDocumentService.GetAllAsync(CancellationToken cancellationToken)
        {
            var documents = await documentReadRepository.GetAllAsync(cancellationToken);
            var persons = await personReadRepository.GetByIdsAsync(documents.Select(x => x.PersonId).Distinct(), cancellationToken);
            var result = new List<DocumentModel>();
            foreach (var document in documents)
            {
                persons.TryGetValue(document.PersonId, out var person);
                var doc = mapper.Map<DocumentModel>(document);
                doc.Person = person != null
                    ? mapper.Map<PersonModel>(person)
                    : null;
                result.Add(doc);
            }
            return result;
        }

        async Task<DocumentModel?> IDocumentService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await documentReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            var person = await personReadRepository.GetByIdAsync(item.PersonId, cancellationToken);

            var docPerson = mapper.Map<DocumentModel>(person);
            var document = mapper.Map<DocumentModel>(item);
            document.Person = docPerson.Person;
            return document;
        }
    }
}
