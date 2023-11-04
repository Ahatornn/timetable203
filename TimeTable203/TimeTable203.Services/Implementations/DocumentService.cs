using AutoMapper;
using Serilog;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Implementations
{
    public class DocumentService : IDocumentService, IServiceAnchor
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
    }
}
