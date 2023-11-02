using AutoMapper;
using Serilog;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Services.Anchors;
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
            Log.Information("Сработал запрос IDisciplineService.GetAllAsync");
            return mapper.Map<IEnumerable<DisciplineModel>>(result);
        }

        async Task<DisciplineModel?> IDisciplineService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await disciplineReadRepository.GetByIdAsync(id, cancellationToken);
            Log.Information("Сработал запрос IDisciplineService.GetByIdAsync");
            if (item == null)
            {
                Log.Warning("Запрос вернул null");
                return null;
            }
            return mapper.Map<DisciplineModel>(item);
        }
    }
}
