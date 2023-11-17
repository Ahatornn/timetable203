﻿using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Models;
using TimeTable203.Api.ModelsRequest.Document;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.ModelsRequest;

namespace TimeTable203.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работу с Документами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Document")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService documentService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DocumentController"/>
        /// </summary>
        public DocumentController(IDocumentService documentService,
            IMapper mapper)
        {
            this.documentService = documentService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех документов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DocumentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await documentService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<DocumentResponse>>(result));
        }

        /// <summary>
        /// Получает документ по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await documentService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти документ с идентификатором {id}");
            }

            return Ok(mapper.Map<DocumentResponse>(item));
        }

        /// <summary>
        /// Получить список всех документов по идентификатору пользователя
        /// </summary>
        [HttpGet("person/{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<DocumentResponse>), StatusCodes.Status200OK)]
        public Task<IActionResult> GetForPerson(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создаёт новый документ
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateDocumentRequest request, CancellationToken cancellationToken)
        {
            var documentRequestModel = mapper.Map<DocumentRequestModel>(request);
            var result = await documentService.AddAsync(documentRequestModel, cancellationToken);
            return Ok(mapper.Map<DocumentResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющийся документ
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(Guid id, DocumentRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<DocumentModel>(request);
            model.Id = id;
            var result = await documentService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<DocumentResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющийся документ по id
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await documentService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
