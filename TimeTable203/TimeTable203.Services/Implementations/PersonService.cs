using AutoMapper;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Services.Contracts.Exceptions;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.ModelsRequest;

namespace TimeTable203.Services.Implementations
{
    public class PersonService : IPersonService, IServiceAnchor
    {
        private readonly IPersonReadRepository personReadRepository;
        private readonly IPersonWriteRepository personWriteRepository;
        private readonly IUnitOfWork unitOfWork;

        private readonly IGroupReadRepository groupReadRepository;
        private readonly IMapper mapper;

        public PersonService(IPersonReadRepository personReadRepository,
            IPersonWriteRepository personWriteRepository,
            IUnitOfWork unitOfWork,
            IGroupReadRepository groupReadRepository,
            IMapper mapper)
        {
            this.personReadRepository = personReadRepository;
            this.personWriteRepository = personWriteRepository;
            this.unitOfWork = unitOfWork;
            this.groupReadRepository = groupReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<PersonModel>> IPersonService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await personReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<PersonModel>>(result);
        }

        async Task<PersonModel?> IPersonService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await personReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<PersonModel>(item);
        }

        async Task<PersonModel> IPersonService.AddAsync(PersonRequestModel person, CancellationToken cancellationToken)
        {
            var item = new Person
            {
                Id = Guid.NewGuid(),
                LastName = person.LastName,
                FirstName = person.FirstName,
                Patronymic = person.Patronymic,
                Email = person.Email,
                Phone = person.Phone
            };
            personWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<PersonModel>(item);
        }

        async Task<PersonModel> IPersonService.UpdateGroupAsync(Guid id, Guid groupId, CancellationToken cancellationToken)
        {
            var targetPerson = await personReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetPerson == null)
            {
                throw new TimeTableEntityNotFoundException<Person>(id);
            }

            var group = await groupReadRepository.GetByIdAsync(groupId, cancellationToken);
            if (group == null)
            {
                throw new TimeTableEntityNotFoundException<Group>(groupId);
            }
            targetPerson.GroupId = group.Id;
            targetPerson.Group = group;

            personWriteRepository.Update(targetPerson);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<PersonModel>(targetPerson);
        }

        async Task<PersonModel> IPersonService.EditAsync(PersonRequestModel source, CancellationToken cancellationToken)
        {
            var targetPerson = await personReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetPerson == null)
            {
                throw new TimeTableEntityNotFoundException<Person>(source.Id);
            }

            targetPerson.LastName = source.LastName;
            targetPerson.FirstName = source.FirstName;
            targetPerson.Patronymic = source.Patronymic;
            targetPerson.Email = source.Email;
            targetPerson.Phone = source.Phone;

            personWriteRepository.Update(targetPerson);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<PersonModel>(targetPerson);
        }

        async Task IPersonService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetPerson = await personReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetPerson == null)
            {
                throw new TimeTableEntityNotFoundException<Person>(id);
            }
            if (targetPerson.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Персонаж с идентификатором {id} уже удален");
            }

            personWriteRepository.Delete(targetPerson);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
