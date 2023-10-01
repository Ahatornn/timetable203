using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Models;
using TimeTable203.Api.Models.Enums;
using TimeTable203.Services.Contracts.Interface;

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

        public DocumentController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await documentService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new DocumentResponse
            {
                Id = x.Id,
                Number = x.Number,
                Series = x.Series,
                IssuedAt = x.IssuedAt,
                IssuedBy = x.IssuedBy,
                DocumentType = (DocumentTypesResponse)x.DocumentType,
                PersonId = x.PersonId,
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await documentService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти документ с идентификатором {id}");
            }

            return Ok(new DocumentResponse
            {
                Id = item.Id,
                Number = item.Number,
                Series = item.Series,
                IssuedAt = item.IssuedAt,
                IssuedBy = item.IssuedBy,
                DocumentType = (DocumentTypesResponse)item.DocumentType,
                PersonId = item.PersonId,
            });
        }
    }
}
