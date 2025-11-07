using LibraryManagmentSystem.Application.Feature.Users.Command.NewFolder;
using LibraryManagmentSystem.Application.Feature.Users.Queries.GetAllUser;
using LibraryManagmentSystem.Application.Feature.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpGet("user")]
        public async Task<IActionResult> GetUser()

        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var res = await mediator.Send(new GetUserByIdQuery(user));
            return Ok(res);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdatUser(UpdateUserCommand command)

        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            command.UserId = user;

            var res = await mediator.Send(command);
            return Ok(res);
        }

    }
}
