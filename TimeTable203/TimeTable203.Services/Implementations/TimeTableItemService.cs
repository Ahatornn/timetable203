using AutoMapper;
using Serilog;
using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Services.Anchors;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Implementations
{
    public class TimeTableItemService : ITimeTableItemService, IServiceAnchor
    {
        private readonly ITimeTableItemReadRepository timeTableItemReadRepository;
        private readonly IDisciplineReadRepository disciplineReadRepository;
        private readonly IGroupReadRepository groupReadRepository;

        private readonly IPersonReadRepository personReadRepository;
        private readonly IMapper mapper;

        public TimeTableItemService(ITimeTableItemReadRepository timeTableItemReadRepository,
            IDisciplineReadRepository disciplineReadRepository,
            IGroupReadRepository groupReadRepository,
            IPersonReadRepository personReadRepository,
            IMapper mapper)
        {
            this.timeTableItemReadRepository = timeTableItemReadRepository;
            this.disciplineReadRepository = disciplineReadRepository;
            this.groupReadRepository = groupReadRepository;
            this.personReadRepository = personReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<TimeTableItemModel>> ITimeTableItemService.GetAllAsync(CancellationToken cancellationToken)
        {
            var timeTableItems = await timeTableItemReadRepository.GetAllAsync(cancellationToken);

            var disciplineId = timeTableItems.Select(x => x.DisciplineId).Distinct();
            var groupId = timeTableItems.Select(x => x.GroupId).Distinct();

            var disciplines = await disciplineReadRepository.GetByIdsAsync(disciplineId, cancellationToken);
            var groups = await groupReadRepository.GetByIdsAsync(groupId, cancellationToken);

            var listTimeTableItemModel = new List<TimeTableItemModel>();
            foreach (var timeTableItem in timeTableItems)
            {
                if (!disciplines.TryGetValue(timeTableItem.DisciplineId, out var discipline))
                {
                    Log.Warning("Запрос вернул null(Discipline) ITimeTableItemService.GetAllAsync");
                    continue;
                }
                if (!groups.TryGetValue(timeTableItem.GroupId, out var group))
                {
                    Log.Warning("Запрос вернул null(Discipline) ITimeTableItemService.GetAllAsync");
                    continue;
                }
                if (timeTableItem.TeacherId == null)
                {
                    Log.Warning("Запрос вернул null(TeacherId) ITimeTableItemService.GetAllAsync");
                    continue;
                }
                var timeTable = mapper.Map<TimeTableItemModel>(timeTableItem);
                timeTable.Discipline = mapper.Map<DisciplineModel>(discipline);
                timeTable.Group = mapper.Map<GroupModel>(group);

                var person = await personReadRepository.GetByIdAsync(timeTableItem.TeacherId!.Value, cancellationToken);
                timeTable.Teacher = mapper.Map<PersonModel>(person);
                listTimeTableItemModel.Add(timeTable);
            }

            return listTimeTableItemModel;
        }

        async Task<TimeTableItemModel?> ITimeTableItemService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await timeTableItemReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            var discipline = await disciplineReadRepository.GetByIdAsync(item.DisciplineId, cancellationToken);
            var group = await groupReadRepository.GetByIdAsync(item.GroupId, cancellationToken);
            var timeTable = mapper.Map<TimeTableItemModel>(item);
            timeTable.Discipline = discipline != null
                ? mapper.Map<DisciplineModel>(discipline)
                : null;
            timeTable.Group = group != null
               ? mapper.Map<GroupModel>(group)
               : null;
            if (item.TeacherId != null)
            {
                var person = await personReadRepository.GetByIdAsync((Guid)item.TeacherId, cancellationToken);
                timeTable.Teacher = person != null
                  ? mapper.Map<PersonModel>(person)
                  : null;
            }
            return timeTable;
        }
    }
}
