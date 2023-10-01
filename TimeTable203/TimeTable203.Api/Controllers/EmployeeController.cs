using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Models;
using TimeTable203.Api.Models.Enums;
using TimeTable203.Services.Contracts.Interface;

namespace TimeTable203.Api.Controllers
{

    /// <summary>
    /// CRUD контроллер по работу с Работниками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await employeeService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new EmployeeResponse
            {
                Id = x.Id,
                EmployeeType = (EmployeeTypesResponse)x.EmployeeType,
                Name = $"{x.Person?.LastName} {x.Person?.FirstName} {x.Person?.Patronymic}",
                MobilePhone = x.Person?.Phone ?? string.Empty,
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await employeeService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти работника с идентификатором {id}");
            }

            return Ok(new EmployeeResponse
            {
                Id = item.Id,
                EmployeeType = (EmployeeTypesResponse)item.EmployeeType,
            });
        }
    }
}
