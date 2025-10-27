using LibraryManagmentSystem.Application.Feature.Borrow.Command.BorrowBook;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BorrowController(IMediator mediator) : ControllerBase
    {
        [HttpPost("Borrow/{bookId}")]
        public async Task<IActionResult> BorrowBook(string bookId)
        {
            var borrow = new BorrowBookCommand
            {
                BookId = Guid.Parse(bookId),
                UserId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
            };
            var result = await mediator.Send(borrow);
            return Ok(result);


        }
    }
}
