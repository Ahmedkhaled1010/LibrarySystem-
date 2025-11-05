using LibraryManagmentSystem.Application.Feature.Users.Queries.GetAllUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var res = await mediator.Send(new GetAllUserQuery());
            return Ok(res);
        }
    }
}
