using LibraryManagmentSystem.Shared.DataTransferModel.UserDto;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(string userId) : IRequest<ApiResponse<UserDto>>;

}
