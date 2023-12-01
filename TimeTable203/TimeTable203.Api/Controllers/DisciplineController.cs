using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Attribute;
using TimeTable203.Api.Infrastructures;
using TimeTable203.Api.Models;
using TimeTable203.Api.Models.Exceptions;
using TimeTable203.Api.ModelsRequest.Discipline;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с Дисциплинами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Discipline")]
    public class DisciplineController : ControllerBase
    {
        private readonly IDisciplineService disciplineService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DisciplineController"/>
        /// </summary>
        public DisciplineController(IDisciplineService disciplineService,
            IMapper mapper,
            IApiValidatorService validatorService)
        {
            this.disciplineService = disciplineService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        /// <summary>
        /// Получить список всех дисциплин
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<DisciplineResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await disciplineService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<DisciplineResponse>>(result));
        }

        /// <summary>
        /// Получает дисциплину по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(DisciplineResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var result = await disciplineService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<DisciplineResponse>(result));
        }

        /// <summary>
        /// Создаёт новую дисциплину
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(DisciplineResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateDisciplineRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var result = await disciplineService.AddAsync(request.Name, request.Description, cancellationToken);
            return Ok(mapper.Map<DisciplineResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющуюся дисциплину
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(DisciplineResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(DisciplineRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<DisciplineModel>(request);
            var result = await disciplineService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<DisciplineResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющуюся дисциплину
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ApiOk(typeof(DisciplineResponse))]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await disciplineService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
