using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Features.Roles.Commands.RemoveUserRole
{
    public class RemoveUserRoleCommand : IRequest<ApiResponse<string>>
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
        public RemoveUserRoleCommand(string userId, string roleName)
        {
            UserId = userId;
            RoleName = roleName;
        }
    }
}
