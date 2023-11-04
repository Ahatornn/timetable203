using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Models;
using TimeTable203.Services.Contracts.Interface;

namespace TimeTable203.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работу с элементами расписания
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "TimeTableItem")]
    public class TimeTableItemController : Controller
    {
        private readonly ITimeTableItemService timeTableItemService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableItemController"/>
        /// </summary>
        public TimeTableItemController(ITimeTableItemService timeTableItemService,
            IMapper mapper)
        {
            this.timeTableItemService = timeTableItemService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех занятий на указанный день
        /// </summary>
        [HttpGet("{targetDate:datetime}")]
        [ProducesResponseType(typeof(IEnumerable<TimeTableItemResponse>), StatusCodes.Status200OK)]
        public Task<IActionResult> GetByDate(DateTime targetDate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получает участника по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TimeTableItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
