using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Attribute;
using TimeTable203.Api.Infrastructures.Validator;
using TimeTable203.Api.Models;
using TimeTable203.Api.ModelsRequest.Group;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.ModelsRequest;

namespace TimeTable203.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работы с Группой
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Group")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService groupService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GroupController"/>
        /// </summary>
        public GroupController(IGroupService groupService,
            IMapper mapper,
            IApiValidatorService validatorService)
        {
            this.groupService = groupService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        /// <summary>
        /// Получить список всех групп
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<GroupResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await groupService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<GroupResponse>>(result));
        }

        /// <summary>
        /// Получает группу по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(GroupResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await groupService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти группу с идентификатором {id}");
            }

            return Ok(mapper.Map<GroupResponse>(item));
        }

        /// <summary>
        /// Создаёт новую группу
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(GroupResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateGroupRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var groupRequestModel = mapper.Map<GroupRequestModel>(request);
            var result = await groupService.AddAsync(groupRequestModel, cancellationToken);
            return Ok(mapper.Map<GroupResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющуюся группу
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(GroupResponse))]
        [ApiConflict]
        public async Task<IActionResult> Edit(GroupRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<GroupRequestModel>(request);
            var result = await groupService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<GroupResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющуюся группу
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await groupService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
