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
            var persons = await personReadRepository.GetByIdsAsync(documents.Select(x => x.PersonId).Distinct(),cancellationToken);
            var result = new List<DocumentModel>();
            foreach (var document in documents)
            {
                var person = persons.FirstOrDefault(s => s.Id == document.PersonId);
                //var documentModel = new DocumentModel
                //{
                //    Id = document.Id,
                //    Number = document.Number,
                //    Series = document.Series,
                //    IssuedAt = document.IssuedAt,
                //    IssuedBy = document.IssuedBy,
                //    DocumentType = (DocumentTypesModel)document.DocumentType,
                //    Person = person == null
                //        ? null
                //        : new PersonModel
                //        {
                //            Id = person.Id,
                //            FirstName = person.FirstName,
                //            LastName = person.LastName,
                //            Patronymic = person.Patronymic,
                //            Email = person.Email,
                //            Phone = person.Phone
                //        },
                //};
                //result.Add(documentModel);
               var docPerson = mapper.Map<DocumentModel>(person);
               var doc = mapper.Map<DocumentModel>(document);
               doc.Person = docPerson.Person;
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
