using AutoMapper;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Implementations
{
    public class DisciplineService : IDisciplineService, IServiceAnchor
    {
        private readonly IDisciplineReadRepository disciplineReadRepository;
        private readonly IMapper mapper;

        public DisciplineService(IDisciplineReadRepository disciplineReadRepository,
            IMapper mapper)
        {
            this.disciplineReadRepository = disciplineReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<DisciplineModel>> IDisciplineService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await disciplineReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<DisciplineModel>>(result);
        }

        async Task<DisciplineModel?> IDisciplineService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await disciplineReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<DisciplineModel>(item);
        }
    }
}
