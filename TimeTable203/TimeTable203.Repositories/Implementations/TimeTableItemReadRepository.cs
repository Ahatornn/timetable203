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

        Task<List<TimeTableItem>> ITimeTableItemReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<TimeTableItem>()
                .NotDeletedAt()
                .OrderBy(x => x.StartDate)
                .ToListAsync(cancellationToken);

        Task<TimeTableItem?> ITimeTableItemReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<TimeTableItem>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
