using LibraryManagmentSystem.Application.Feature.Borrow.Command.BorrowBook;
using LibraryManagmentSystem.Application.Feature.Borrow.Command.ResponsBorrowBook;
using LibraryManagmentSystem.Application.Feature.Borrow.Command.ReturnBook;
using LibraryManagmentSystem.Application.Feature.Borrow.Queries.GetAllBorrowByUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BorrowController(IMediator mediator) : ControllerBase
    {
        [HttpPost("request-borrow/{bookId}")]
        public async Task<IActionResult> BorrowBook(string bookId)
        {
            var borrow = new BorrowBookCommand
            {
                BookId = Guid.Parse(bookId),
                UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            };
            var result = await mediator.Send(borrow);
            return Ok(result);


        }
        [HttpPost("Return/{bookId}")]
        public async Task<IActionResult> ReturnBook(string bookId)
        {
            var returnBook = new ReturnBookCommand
            {
                BookId = Guid.Parse(bookId),
                UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            };
            var result = await mediator.Send(returnBook);
            return Ok(result);
        }


        [HttpPost("ResponseBorrow")]
        public async Task<IActionResult> ResponsBorrowBook([FromBody] ResponsBorrowBookCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("BorrowingHistory")]
        public async Task<IActionResult> BorrowingHistory()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var query = new GetAllBorrowByUserQuery
            {
                UserId = userId
            };
            var result = await mediator.Send(query);
            return Ok(result);
        }

    }
}
