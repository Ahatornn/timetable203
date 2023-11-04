using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Models;
using TimeTable203.Services.Contracts.Interface;

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await disciplineService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти дисциплину с идентификатором {id}");
            }

            return Ok(mapper.Map<DisciplineResponse>(result));
        }
    }
}
