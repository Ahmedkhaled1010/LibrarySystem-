using LibraryManagmentSystem.Application.Feature.Borrow.Queries.GetAllRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{status}")]
        public async Task<IActionResult> GetRequests(string status)
        {
            var request = new GetAllRequestQuery(status);
            var result = await mediator.Send(request);
            return Ok(result);
        }

    }
}
