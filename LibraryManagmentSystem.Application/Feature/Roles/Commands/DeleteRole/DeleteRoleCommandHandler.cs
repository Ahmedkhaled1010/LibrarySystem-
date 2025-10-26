using LibraryManagmentSystem.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler(RoleManager<IdentityRole> roleManager) : IRequestHandler<DeleteRoleCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteRoleCommandValidator();
            var result = await validator.ValidateAsync(request);
            if (result.Errors.Any())
            {

                return ApiResponse<string>.Fail("Validation Failed");

            }
            var role = await roleManager.FindByIdAsync(request.Id);
            if (role == null)
            {

                return ApiResponse<string>.Fail("Role not found");

            }
            var Result = await roleManager.DeleteAsync(role);
            if (Result.Succeeded)
            {
                return ApiResponse<string>.Ok(request.Id, "Role Deleted Successfully");
            }
            else
            {
                return ApiResponse<string>.Fail("Role Deletion Failed");
            }
        }
    }
}
