using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Features.Roles.Queries.GetAllUserRoles
{
    public class GetAllUserRolesQuery : IRequest<ApiResponse<IEnumerable<string>>>
    {
        public string UserId { get; set; }
        public GetAllUserRolesQuery(string userId)
        {
            UserId = userId;
        }
    }
}
