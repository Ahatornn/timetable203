using TimeTable203.Context.Contracts;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts.Interface;

namespace TimeTable203.Repositories.Implementations
{
    public class TimeTableItemReadRepository : ITimeTableItemReadRepository
    {
        private readonly ITimeTableContext context;

        public TimeTableItemReadRepository(ITimeTableContext context)
        {
            this.context = context;
        }

        Task<List<TimeTableItem>> ITimeTableItemReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.TimeTableItems.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.StartDate)
                .ToList());

        Task<TimeTableItem?> ITimeTableItemReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.TimeTableItems.FirstOrDefault(x => x.Id == id));
    }
}
