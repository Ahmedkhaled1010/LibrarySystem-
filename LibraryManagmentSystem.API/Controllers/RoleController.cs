using LibraryManagmentSystem.Application.Features.Roles.Commands.CreateRole;
using LibraryManagmentSystem.Application.Features.Roles.Commands.DeleteRole;
using LibraryManagmentSystem.Application.Features.Roles.Commands.RemoveUserRole;
using LibraryManagmentSystem.Application.Features.Roles.Commands.UpdateRole;
using LibraryManagmentSystem.Application.Features.Roles.Queries.GetAllRoles;
using LibraryManagmentSystem.Application.Features.Roles.Queries.GetAllUserRoles;
using LibraryManagmentSystem.Application.Features.Roles.Queries.GetRoleById;
using LibraryManagmentSystem.Applicationr.Features.Roles.Commands.AssignRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class RoleController(IMediator mediator) : ControllerBase
    {
        [HttpGet("get-all-roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await mediator.Send(new GetAllRolesQuery());
            return Ok(result);
        }
        [HttpGet("get-role-by-id/{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var result = await mediator.Send(new GetRoleByIdQuery(id));
            return Ok(result);
        }
        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand roleName)
        {
            var result = await mediator.Send(roleName);
            return Ok(result);
        }
        [HttpPut("update-role")]
        public async Task<IActionResult> UpdateRole(UpdateRoleCommand roleCommand)
        {
            var result = await mediator.Send(roleCommand);
            return Ok(result);

        }
        [HttpDelete("delete-role/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result = await mediator.Send(new DeleteRoleCommand(id));
            return Ok(result);
        }
        [HttpPost("assign-role-to-user")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommand assignRole)
        {
            var result = await mediator.Send(assignRole);
            return Ok(result);
        }
        [HttpPost("remove-role-from-user")]
        public async Task<IActionResult> RemoveRoleFromUser(RemoveUserRoleCommand removeRole)
        {
            var result = await mediator.Send(removeRole);
            return Ok(result);
        }
        [HttpGet("get-user-roles")]
        public async Task<IActionResult> GetUserRoles([FromQuery] string userId)
        {
            var result = await mediator.Send(new GetAllUserRolesQuery(userId));
            return Ok(result);
        }
    }
}
