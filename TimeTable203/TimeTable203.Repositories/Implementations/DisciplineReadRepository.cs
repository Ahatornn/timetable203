using TimeTable203.Context.Contracts;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts.Interface;

namespace TimeTable203.Repositories.Implementations
{
    public class DisciplineReadRepository : IDisciplineReadRepository
    {
        private readonly ITimeTableContext context;

        public DisciplineReadRepository(ITimeTableContext context)
        {
            this.context = context;
        }

        Task<List<Discipline>> IDisciplineReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Disciplines.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Name)
                .ToList());

        Task<Discipline?> IDisciplineReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Disciplines.FirstOrDefault(x => x.Id == id));
    }
}
