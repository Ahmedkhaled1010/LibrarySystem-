using AutoMapper;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.Application.Features.Roles.Queries.GetAllUserRoles
{
    public class GetAllUserRolesQueryHandler(UserManager<User> userManager, IMapper mapper) : IRequestHandler<GetAllUserRolesQuery, ApiResponse<IEnumerable<string>>>
    {
        public async Task<ApiResponse<IEnumerable<string>>> Handle(GetAllUserRolesQuery request, CancellationToken cancellationToken)
        {
            var validtor = new GetAllUserRolesQueryValidator();
            var validationResult = await validtor.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));

                return ApiResponse<IEnumerable<string>>.Fail($"Validation Failed: {errors}");
            }
            var user = await userManager.FindByIdAsync(request.UserId);
            var roles = await userManager.GetRolesAsync(user);
            return ApiResponse<IEnumerable<string>>.Ok(roles, "User Roles Retrieved Successfully");

        }
    }
}
