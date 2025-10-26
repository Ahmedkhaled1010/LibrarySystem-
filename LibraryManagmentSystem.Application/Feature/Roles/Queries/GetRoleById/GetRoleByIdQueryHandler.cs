using AutoMapper;
using LibraryManagmentSystem.Shared.DataTransferModel.Role;
using LibraryManagmentSystem.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.Application.Features.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQueryHandler(RoleManager<IdentityRole> roleManager,
        IMapper mapper) : IRequestHandler<GetRoleByIdQuery, ApiResponse<RoleDto>>
    {
        public async Task<ApiResponse<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await roleManager.FindByIdAsync(request.Id);
            if (role == null)
            {
                return ApiResponse<RoleDto>.Fail("Role not found");
            }
            var roleDto = mapper.Map<IdentityRole, RoleDto>(role);
            return ApiResponse<RoleDto>.Ok(roleDto, $"Role With {request.Id} Retrived Successfuly");
        }
    }
}
