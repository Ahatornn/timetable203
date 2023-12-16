using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Attribute;
using TimeTable203.Api.Infrastructures.Validator;
using TimeTable203.Api.Models;
using TimeTable203.Api.ModelsRequest.TimeTableItem;
using TimeTable203.Api.ModelsRequest.TimeTableItemRequest;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.ModelsRequest;

namespace TimeTable203.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работу с элементами расписания
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "TimeTableItem")]
    public class TimeTableItemController : ControllerBase
    {
        private readonly ITimeTableItemService timeTableItemService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableItemController"/>
        /// </summary>
        public TimeTableItemController(ITimeTableItemService timeTableItemService,
            IMapper mapper,
            IApiValidatorService validatorService)
        {
            this.timeTableItemService = timeTableItemService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        /// <summary>
        /// Получить список всех занятий на указанный день
        /// </summary>
        [HttpGet("{targetDate:datetime}")]
        [ApiOk(typeof(IEnumerable<TimeTableItemResponse>))]
        public async Task<IActionResult> GetByDate(DateTime targetDate, CancellationToken cancellationToken)
        {
            var items = await timeTableItemService.GetAllAsync(targetDate, cancellationToken);
            return Ok(mapper.Map<IEnumerable<TimeTableItemResponse>>(items));
        }

        /// <summary>
        /// Получает участника по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(TimeTableItemResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
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
        [ApiOk(typeof(TimeTableItemResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateTimeTableItemRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var timeTableRequestModel = mapper.Map<TimeTableItemRequestModel>(request);
            var result = await timeTableItemService.AddAsync(timeTableRequestModel, cancellationToken);
            return Ok(mapper.Map<TimeTableItemResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющееся расписание
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(TimeTableItemResponse))]
        [ApiConflict]
        public async Task<IActionResult> Edit(TimeTableItemRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<TimeTableItemRequestModel>(request);
            var result = await timeTableItemService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<TimeTableItemResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющееся расписание по id
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await timeTableItemService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
