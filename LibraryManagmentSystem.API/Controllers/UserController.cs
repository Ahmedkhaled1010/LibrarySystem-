using LibraryManagmentSystem.Application.Feature.Users.Command.ChangePassword;
using LibraryManagmentSystem.Application.Feature.Users.Command.NewFolder;
using LibraryManagmentSystem.Application.Feature.Users.Command.UploadProfileImage;
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
    [Authorize]
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

        public async Task<IActionResult> UpdatUser(UpdateUserCommand command)

        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            command.UserId = user;

            var res = await mediator.Send(command);
            return Ok(res);
        }
        [HttpPut("chanege-password")]

        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)

        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            command.UserId = user;

            var res = await mediator.Send(command);
            return Ok(res);
        }
        [HttpPost("uploaf-profile-image")]
        public async Task<IActionResult> Uploadmage([FromForm] IFormFile file)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var command = new UploadProfileImageCommand
            {
                file = file,
                UserId = user

            };
            var result = await mediator.Send(command);
            return Ok(result);
        }

    }
}
