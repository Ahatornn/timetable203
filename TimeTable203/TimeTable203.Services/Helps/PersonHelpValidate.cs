using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Services.Contracts.Exceptions;

namespace TimeTable203.Services.Helps
{
    public class PersonHelpValidate
    {
        private readonly IPersonReadRepository personReadRepository;
        public PersonHelpValidate(IPersonReadRepository personReadRepository)
        {
            this.personReadRepository = personReadRepository;
        }
        async public Task<Person?> GetPersonByIdAsync(Guid id_person, CancellationToken cancellationToken)
        {
            if (id_person != Guid.Empty)
            {
                var targetPerson = await personReadRepository.GetByIdAsync(id_person, cancellationToken);
                if (targetPerson == null)
                {
                    throw new TimeTableEntityNotFoundException<Person>(id_person);
                }
                return targetPerson;
            }
            return null;
        }
    }
}
