using LibraryManagmentSystem.Shared.DataTransferModel.Role;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<ApiResponse<RoleDto>>
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public UpdateRoleCommand(string id, string roleName)
        {
            Id = id;
            RoleName = roleName;
        }
    }
}
