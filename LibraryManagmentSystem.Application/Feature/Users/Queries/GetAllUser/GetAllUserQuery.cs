using LibraryManagmentSystem.Shared.DataTransferModel.UserDto;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Users.Queries.GetAllUser
{
    public record GetAllUserQuery : IRequest<ApiResponse<IEnumerable<UserDto>>>;


}
