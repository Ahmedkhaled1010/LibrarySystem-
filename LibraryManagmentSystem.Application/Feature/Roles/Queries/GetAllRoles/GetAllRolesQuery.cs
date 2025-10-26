using LibraryManagmentSystem.Shared.DataTransferModel.Role;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Features.Roles.Queries.GetAllRoles
{
    public record GetAllRolesQuery : IRequest<ApiResponse<IEnumerable<RoleDto>>>;


}
