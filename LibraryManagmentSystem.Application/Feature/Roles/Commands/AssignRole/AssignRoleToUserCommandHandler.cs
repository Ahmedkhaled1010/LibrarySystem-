using LibraryManagmentSystem.Application.Commands.AssignRole;
using LibraryManagmentSystem.Applicationr.Features.Roles.Commands.AssignRole;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.Application.Features.Roles.Commands.AssignRole
{
    public class AssignRoleToUserCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignRoleToUserCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new AssignRoleToUserCommandValidator(userManager, roleManager);
            var result = await validator.ValidateAsync(request);
            if (result.Errors.Any())
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));

                return ApiResponse<string>.Fail($"Validation Failed: {errors}");

            }
            var user = await userManager.FindByIdAsync(request.UserId);
            var assignRoleResult = await userManager.AddToRoleAsync(user, request.RoleName);
            if (assignRoleResult.Succeeded)
            {
                return ApiResponse<string>.Ok(request.RoleName, "Role Assigned Successfully");
            }
            else
            {
                return ApiResponse<string>.Fail("Role Assignment Failed");
            }

        }
    }
}
