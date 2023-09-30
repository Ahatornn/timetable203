using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Implementations
{
    public class DisciplineService: IDisciplineService
    {
        private readonly IDisciplineReadRepository disciplineReadRepository;

        public DisciplineService(IDisciplineReadRepository disciplineReadRepository)
        {
            this.disciplineReadRepository = disciplineReadRepository;
        }

        async Task<IEnumerable<DisciplineModel>> IDisciplineService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await disciplineReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new DisciplineModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            });
        }

        async Task<DisciplineModel?> IDisciplineService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await disciplineReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return new DisciplineModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
            };
        }
    }
}
