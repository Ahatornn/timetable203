﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Models;
using TimeTable203.Services.Contracts.Interface;

namespace TimeTable203.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работы с Группой
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Group")]
    public class GroupController : Controller
    {
        private readonly IGroupService groupService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GroupController"/>
        /// </summary>
        public GroupController(IGroupService groupService,
            IMapper mapper)
        {
            this.groupService = groupService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех групп
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GroupResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await groupService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<GroupResponse>(result));
        }

        /// <summary>
        /// Получает группу по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await groupService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти группу с идентификатором {id}");
            }

            return Ok(mapper.Map<GroupResponse>(item));
        }
    }
}
