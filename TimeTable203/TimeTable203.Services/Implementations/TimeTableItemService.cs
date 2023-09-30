using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Services.Contracts.Interface;

namespace TimeTable203.Services.Implementations
{
    public class TimeTableItemService : ITimeTableItemService
    {
        private readonly ITimeTableItemReadRepository timeTableItemReadRepository;

        public TimeTableItemService(ITimeTableItemReadRepository timeTableItemReadRepository)
        {
            this.timeTableItemReadRepository = timeTableItemReadRepository;
        }

        async Task<IEnumerable<TimeTableItemModel>> ITimeTableItemService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await timeTableItemReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new TimeTableItemModel
            {
                Id = x.Id,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                DisciplineId = x.DisciplineId,
                GroupId = x.GroupId,
                RoomNumber = x.RoomNumber,
                Teacher = x.Teacher,
            });
        }

        async Task<TimeTableItemModel?> ITimeTableItemService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await timeTableItemReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return new TimeTableItemModel
            {
                Id = item.Id,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                DisciplineId = item.DisciplineId,
                GroupId = item.GroupId,
                RoomNumber = item.RoomNumber,
                Teacher = item.Teacher,
            };
        }
    }
}
