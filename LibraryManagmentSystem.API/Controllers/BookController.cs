using LibraryManagmentSystem.Application.Feature.Books.Command.CreateBook;
using LibraryManagmentSystem.Application.Feature.Books.Command.DeleteBook;
using LibraryManagmentSystem.Application.Feature.Books.Command.UpdateBook;
using LibraryManagmentSystem.Application.Feature.Books.Queries.GetAllBook;
using LibraryManagmentSystem.Application.Feature.Books.Queries.GetAllBookPurchase;
using LibraryManagmentSystem.Application.Feature.Books.Queries.GetBookById;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.QueryParams;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController(IMediator mediator, IServicesManager servicesManager) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]

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
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateBook(UpdateBookCommand updateBook)
        {
            var result = await mediator.Send(updateBook);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var result = await mediator.Send(new DeleteBookCommand(id));
            return Ok(result);
        }
        [HttpGet("Purchase")]
        public async Task<IActionResult> GetBookPurchaseById()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await mediator.Send(new GetAllBookPurchaseQuery(user));
            return Ok(result);
        }
        [HttpPost("reserve/{bookId}")]
        public async Task<IActionResult> ReserveBook(Guid bookId)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await servicesManager.ReservationServices.CreateReservation(user, bookId);
            return Ok();
        }
    }
}
