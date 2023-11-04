using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Models;
using TimeTable203.Services.Contracts.Interface;

namespace TimeTable203.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с Участниками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Person")]
    public class PersonController : Controller
    {
        private readonly IPersonService personService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PersonController"/>
        /// </summary>
        public PersonController(IPersonService personService,
            IMapper mapper)
        {
            this.personService = personService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех участников
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await personService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<PersonResponse>>(result));
        }

        /// <summary>
        /// Получает участника по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await personService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти участника с идентификатором {id}");
            }

            return Ok(mapper.Map<PersonResponse>(item));
        }
    }
}
