using LibraryManagmentSystem.Application.Feature.Books.Command.CreateBook;
using LibraryManagmentSystem.Application.Feature.Books.Command.DeleteBook;
using LibraryManagmentSystem.Application.Feature.Books.Command.UpdateBook;
using LibraryManagmentSystem.Application.Feature.Books.Queries.GetAllBook;
using LibraryManagmentSystem.Application.Feature.Books.Queries.GetBookById;
using LibraryManagmentSystem.Shared.QueryParams;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin,PUBLISHER")]
        public async Task<IActionResult> CreateBook(CreateBookCommand createBook)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            createBook.AuthorId = user;
            var result = await mediator.Send(createBook);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] BookQueryParams bookQueryParams)
        {

            var result = await mediator.Send(new GetAllBookQuery(bookQueryParams));
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(string id)
        {
            var result = await mediator.Send(new GetBookByIdQuery(id));
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBook(UpdateBookCommand updateBook)
        {
            var result = await mediator.Send(updateBook);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var result = await mediator.Send(new DeleteBookCommand(id));
            return Ok(result);
        }
    }
}
