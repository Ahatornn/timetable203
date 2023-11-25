using AutoMapper;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Services.Contracts.Exceptions;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.FluentValidation;

namespace TimeTable203.Services.Implementations
{
    /// <inheritdoc cref="IDisciplineService"/>
    public class DisciplineService : IDisciplineService, IServiceAnchor
    {
        private readonly IDisciplineReadRepository disciplineReadRepository;
        private readonly IDisciplineWriteRepository disciplineWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly DisciplineValidator disciplineValidator;

        public DisciplineService(IDisciplineReadRepository disciplineReadRepository,
            IDisciplineWriteRepository disciplineWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.disciplineReadRepository = disciplineReadRepository;
            this.disciplineWriteRepository = disciplineWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            disciplineValidator = new DisciplineValidator();
        }

        async Task<IEnumerable<DisciplineModel>> IDisciplineService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await disciplineReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<DisciplineModel>>(result);
        }

        async Task<DisciplineModel> IDisciplineService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await disciplineReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new TimeTableEntityNotFoundException<Discipline>(id);
            }
            return mapper.Map<DisciplineModel>(item);
        }

        async Task<DisciplineModel> IDisciplineService.AddAsync(string name, string description, CancellationToken cancellationToken)
        {
            var item = new Discipline
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
            };

            disciplineWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<DisciplineModel>(item);
        }

        async Task<DisciplineModel> IDisciplineService.EditAsync(DisciplineModel source, CancellationToken cancellationToken)
        {
            var targetDiscipline = await disciplineReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetDiscipline == null)
            {
                throw new TimeTableEntityNotFoundException<Discipline>(source.Id);
            }

            targetDiscipline.Name = source.Name;
            targetDiscipline.Description = source.Description;
            disciplineWriteRepository.Update(targetDiscipline);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<DisciplineModel>(targetDiscipline);
        }

        async Task IDisciplineService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetDiscipline = await disciplineReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetDiscipline == null)
            {
                throw new TimeTableEntityNotFoundException<Discipline>(id);
            }

            if (targetDiscipline.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Дисциплина с идентификатором {id} уже удалена");
            }

            disciplineWriteRepository.Delete(targetDiscipline);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
