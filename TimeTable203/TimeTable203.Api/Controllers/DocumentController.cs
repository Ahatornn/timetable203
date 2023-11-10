using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Models;
using TimeTable203.Api.ModelsRequest;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.ModelsRequest;
using TimeTable203.Services.Implementations;

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
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
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
        public Task<IActionResult> GetForPerson([Required] Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создаёт новый документ
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([Required] Guid personId, CreateDocumentRequest request, CancellationToken cancellationToken)
        {
            var documentRequestModel = mapper.Map<DocumentRequestModel>(request);
            var result = await documentService.AddAsync(personId, documentRequestModel, cancellationToken);
            return Ok(mapper.Map<DocumentResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющийся документ
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit([Required] Guid id, CreateDocumentRequest request, CancellationToken cancellationToken, Guid personId = default)
        {
            var modelRequest = mapper.Map<DocumentRequestModel>(request);
            var model = mapper.Map<DocumentModel>(modelRequest);
            model.Id = id;
            var result = await documentService.EditAsync(personId, model, cancellationToken);
            return Ok(mapper.Map<DocumentResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющийся документ по id
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await documentService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
