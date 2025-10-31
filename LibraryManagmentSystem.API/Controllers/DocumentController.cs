using LibraryManagmentSystem.Application.Feature.Documents.Commands.UploadDocument;
using LibraryManagmentSystem.Application.Feature.Documents.Queries.GetDocument;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController(IMediator mediator) : ControllerBase
    {
        [HttpPost("upload-image/{bookId}")]
        [Authorize(Roles = "Admin,PUBLISHER")]
        public async Task<IActionResult> UploadCoverImage([FromForm] IFormFile file, Guid bookId)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var command = new UploadDocumentCommand
            {
                file = file,
                folderName = "Images",
                bookId = bookId,
                AuthorId = user
            };
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("upload-pdf/{bookId}")]
        [Authorize(Roles = "Admin,PUBLISHER")]

        public async Task<IActionResult> UploadPdf([FromForm] IFormFile file, Guid bookId)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var command = new UploadDocumentCommand
            {
                file = file,
                folderName = "Images",
                bookId = bookId,
                AuthorId = user
            };
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("preview-document")]
        public async Task<IActionResult> PreviewDocument([FromQuery] GetDocumentQuery documentQuery)
        {

            var result = await mediator.Send(documentQuery);
            return Ok(result);
        }
        [HttpGet("get-document")]
        public async Task<IActionResult> GetDocument([FromQuery] GetDocumentQuery documentQuery)
        {

            var result = await mediator.Send(documentQuery);
            return Ok(result);
        }
    }
}
