using Microsoft.EntityFrameworkCore;
using Serilog;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Common.Entity.Repositories;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Repositories.Implementations
{
    public class TimeTableItemReadRepository : ITimeTableItemReadRepository, IRepositoryAnchor
    {
        private readonly IDbRead reader;

        public TimeTableItemReadRepository(IDbRead reader)
        {
            this.reader = reader;
            Log.Information("Инициализирован абстракция IDbReader в классе TimeTableItemReadRepository");
        }

        Task<IReadOnlyCollection<TimeTableItem>> ITimeTableItemReadRepository.GetAllByDateAsync(DateTimeOffset startDate,
            DateTimeOffset endDate,
            CancellationToken cancellationToken)
            => reader.Read<TimeTableItem>()
                .NotDeletedAt()
                .Where(x => x.StartDate >= startDate &&
                            x.EndDate <= endDate)
                .OrderBy(x => x.StartDate)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<TimeTableItem?> ITimeTableItemReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<TimeTableItem>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
