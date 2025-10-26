using LibraryManagmentSystem.Shared.DataTransferModel.Role;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<ApiResponse<RoleDto>>
    {
        public string RoleName { get; set; }
        public CreateRoleCommand(string roleName)
        {
            RoleName = roleName;

        }
    }
}
