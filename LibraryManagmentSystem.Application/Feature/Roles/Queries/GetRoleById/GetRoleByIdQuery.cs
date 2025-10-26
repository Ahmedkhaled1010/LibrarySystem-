using LibraryManagmentSystem.Shared.DataTransferModel.Role;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Features.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQuery : IRequest<ApiResponse<RoleDto>>
    {
        public string Id { get; set; }
        public GetRoleByIdQuery(string id)
        {
            Id = id;
        }
    }
}
