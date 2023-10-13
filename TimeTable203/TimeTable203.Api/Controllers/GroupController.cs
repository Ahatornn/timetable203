using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Models;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работу с Группой
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Group")]
    public class GroupController : Controller
    {
        private readonly IGroupService groupService;

        public GroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await groupService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new GroupResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Students = x.Students.Select(y => $"{y.LastName} {y.FirstName} {y.Phone} {y.Id}").ToList(),
                FIO = $"{x.Employee?.LastName} {x.Employee?.FirstName} {x.Employee?.Patronymic}",
                MobilePhone = x.Employee?.Phone ?? string.Empty
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await groupService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти группу с идентификатором {id}");
            }

            return Ok(new GroupResponse
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Students = item.Students.Select(y => $"{y.LastName} {y.FirstName} {y.Phone}").ToList(),
                FIO = $"{item.Employee?.LastName} {item.Employee?.FirstName} {item.Employee?.Patronymic}",
                MobilePhone = item.Employee?.Phone ?? string.Empty
            });
        }
    }
}
