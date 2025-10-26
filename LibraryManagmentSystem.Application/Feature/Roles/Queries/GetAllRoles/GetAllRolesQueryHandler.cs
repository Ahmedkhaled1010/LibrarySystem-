using AutoMapper;
using LibraryManagmentSystem.Shared.DataTransferModel.Role;
using LibraryManagmentSystem.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Application.Features.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQueryHandler(RoleManager<IdentityRole> roleManager, IMapper mapper) : IRequestHandler<GetAllRolesQuery, ApiResponse<IEnumerable<RoleDto>>>
    {
        public async Task<ApiResponse<IEnumerable<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var Roles = await roleManager.Roles.ToListAsync();
            var Result = mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleDto>>(Roles);

            return ApiResponse<IEnumerable<RoleDto>>.Ok(Result, "Roles Retrived Successfuly");
        }
    }
}
