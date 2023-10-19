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

        public TimeTableItemController(ITimeTableItemService timeTableItemService)
        {
            this.timeTableItemService = timeTableItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await timeTableItemService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new TimeTableItemResponse
            {
                Id = x.Id,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                NameDiscipline = x.Discipline?.Name ?? string.Empty,
                NameGroup = x.Group?.Name ?? string.Empty,
                RoomNumber = x.RoomNumber,
                NamePerson = $"{x.Teacher?.LastName} {x.Teacher?.FirstName} {x.Teacher?.Patronymic}",
                Phone = x.Teacher?.Phone ?? string.Empty
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await timeTableItemService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти элемент расписания с идентификатором {id}");
            }

            return Ok(new TimeTableItemResponse
            {

                Id = item.Id,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                NameDiscipline = item.Discipline?.Name ?? string.Empty,
                NameGroup = item.Group?.Name ?? string.Empty,
                RoomNumber = item.RoomNumber,
                NamePerson = $"{item.Teacher?.LastName} {item.Teacher?.FirstName} {item.Teacher?.Patronymic}",
                Phone = item.Teacher?.Phone ?? string.Empty
            });
        }
    }
}
