using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Applicationr.Features.Roles.Commands.AssignRole
{
    public class AssignRoleToUserCommand : IRequest<ApiResponse<string>>
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
        public AssignRoleToUserCommand(string userId, string roleName)
        {
            UserId = userId;
            RoleName = roleName;
        }
    }
}
