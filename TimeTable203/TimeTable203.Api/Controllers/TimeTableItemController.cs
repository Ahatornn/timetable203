using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Models;
using TimeTable203.Api.ModelsRequest;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.ModelsRequest;

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
        public async Task<IActionResult> GetByDate(DateTime targetDate, CancellationToken cancellationToken)
        {
            var items = await timeTableItemService.GetAllAsync(targetDate, cancellationToken);
            return Ok(mapper.Map<IEnumerable<TimeTableItemResponse>>(items));
        }

        /// <summary>
        /// Получает участника по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TimeTableItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await timeTableItemService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти элемент расписания с идентификатором {id}");
            }

            return Ok(mapper.Map<TimeTableItemResponse>(item));
        }

        /// <summary>
        /// Создаёт новое расписание
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TimeTableItemResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([Required] Guid id_discipline, [Required] Guid id_group, [Required] Guid id_teacher, CreateTimeTableItemRequest request, CancellationToken cancellationToken)
        {
            var timeTableRequestModel = mapper.Map<TimeTableItemRequestModel>(request);
            var result = await timeTableItemService.AddAsync(id_discipline, id_group, id_teacher, timeTableRequestModel, cancellationToken);
            return Ok(mapper.Map<TimeTableItemResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющееся расписание
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(TimeTableItemResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit([Required] Guid id, CreateDocumentRequest request, CancellationToken cancellationToken, Guid id_discipline = default, Guid id_group = default, Guid id_teacher = default)
        {
            var modelRequest = mapper.Map<TimeTableItemRequestModel>(request);
            var model = mapper.Map<TimeTableItemModel>(modelRequest);
            model.Id = id;
            var result = await timeTableItemService.EditAsync(id_discipline, id_group, id_teacher, model, cancellationToken);
            return Ok(mapper.Map<TimeTableItemResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющееся расписание по id
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await timeTableItemService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
