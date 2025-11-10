using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Users.Command.DeleteUser
{
    public record DeleteUserCommand(string UserId) : IRequest<ApiResponse<string>>;


}
