using Microsoft.AspNetCore.Mvc;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Services.Contracts.Interface;

namespace TimeTable203.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работу с элементами расписания
    /// </summary>
    [ApiController]
    [Route("[controller]")]
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
                DisciplineId = x.DisciplineId,
                GroupId = x.GroupId,
                RoomNumber = x.RoomNumber,
                Teacher = x.Teacher,
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
                DisciplineId = item.DisciplineId,
                GroupId = item.GroupId,
                RoomNumber = item.RoomNumber,
                Teacher = item.Teacher,
            });
        }
    }
}
