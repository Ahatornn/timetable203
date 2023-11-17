using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Models;
using TimeTable203.Api.ModelsRequest.Discipline;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Services.Contracts.Exceptions;
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
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DisciplineController"/>
        /// </summary>
        public DisciplineController(IDisciplineService disciplineService,
            IMapper mapper)
        {
            this.disciplineService = disciplineService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех дисциплин
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DisciplineResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await disciplineService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<DisciplineResponse>>(result));
        }

        /// <summary>
        /// Получает дисциплину по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(DisciplineResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var result = await disciplineService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<DisciplineResponse>(result));
        }

        /// <summary>
        /// Создаёт новую дисциплину
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(DisciplineResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TimeTableEntityNotFoundException<Discipline>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TimeTableInvalidOperationException), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateDisciplineRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await disciplineService.AddAsync(request.Name, request.Description, cancellationToken);
                return Ok(mapper.Map<DisciplineResponse>(result));
            }
            catch (TimeTableEntityNotFoundException<Discipline> TimeEntityNotFound)
            {
                return NotFound(TimeEntityNotFound.Message);
            }
            catch (TimeTableInvalidOperationException TimeInvalidOperation)
            {
                return BadRequest(TimeInvalidOperation.Message);
            }
        }

        /// <summary>
        /// Редактирует имеющуюся дисциплину
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(DisciplineResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(DisciplineRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<DisciplineModel>(request);
            var result = await disciplineService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<DisciplineResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющуюся дисциплину
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await disciplineService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
