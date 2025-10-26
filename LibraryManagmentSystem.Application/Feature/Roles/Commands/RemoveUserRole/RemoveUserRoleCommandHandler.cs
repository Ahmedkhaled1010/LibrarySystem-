using LibraryManagmentSystem.ApplicationFeatures.Roles.Commands.RemoveUserRole;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.Application.Features.Roles.Commands.RemoveUserRole
{
    public class RemoveUserRoleCommandHandler(RoleManager<IdentityRole> roleManager,
        UserManager<User> userManager
        ) : IRequestHandler<RemoveUserRoleCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            var validator = new RemoveUserRoleCommandValidator(userManager, roleManager);
            var result = await validator.ValidateAsync(request);
            if (result.Errors.Any())
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));

                return ApiResponse<string>.Fail($"Validation Failed: {errors}");

            }
            var user = await userManager.FindByIdAsync(request.UserId);
            var results = await userManager.RemoveFromRoleAsync(user, request.RoleName);
            if (results.Succeeded)
            {
                return ApiResponse<string>.Ok(request.RoleName, "Role Removed Successfully");
            }
            else
            {
                return ApiResponse<string>.Fail("Role Removal Failed");
            }

        }
    }
}
