﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Attribute;
using TimeTable203.Api.Models;
using TimeTable203.Api.Models.Exceptions;
using TimeTable203.Api.ModelsRequest.Employee;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.ModelsRequest;

namespace TimeTable203.Api.Controllers
{

    /// <summary>
    /// CRUD контроллер по работе с сотрудниками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EmployeeController"/>
        /// </summary>
        public EmployeeController(IEmployeeService employeeService,
            IMapper mapper)
        {
            this.employeeService = employeeService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех сотрудников
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<EmployeeResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await employeeService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<EmployeeResponse>>(result));
        }

        /// <summary>
        /// Получает сотрудника по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(EmployeeResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await employeeService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти сотрудника с идентификатором {id}");
            }

            return Ok(mapper.Map<EmployeeResponse>(item));
        }

        /// <summary>
        /// Создаёт нового рабочего
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(EmployeeResponse))]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employeeRequestModel = mapper.Map<EmployeeRequestModel>(request);
            var result = await employeeService.AddAsync(employeeRequestModel, cancellationToken);
            return Ok(mapper.Map<EmployeeResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющищегося рабочего
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(EmployeeResponse))]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Edit(EmployeeRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<EmployeeRequestModel>(request);
            var result = await employeeService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<EmployeeResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющийегося рабочего по id
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status406NotAcceptable)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await employeeService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
