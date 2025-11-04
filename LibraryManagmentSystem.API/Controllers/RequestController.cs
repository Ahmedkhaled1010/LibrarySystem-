using LibraryManagmentSystem.Application.Feature.Borrow.Queries.GetAllRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetRequests()
        {
            var request = new GetAllRequestQuery();
            var result = await mediator.Send(request);
            return Ok(result);
        }

    }
}
