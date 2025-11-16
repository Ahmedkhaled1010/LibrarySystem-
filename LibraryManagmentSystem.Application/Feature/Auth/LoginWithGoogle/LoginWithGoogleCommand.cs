using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Auth.LoginWithGoogle
{
    public record LoginWithGoogleCommand(string token) : IRequest<ApiResponse<bool>>;

}
