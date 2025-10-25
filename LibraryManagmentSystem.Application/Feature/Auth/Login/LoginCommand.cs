using LibraryManagmentSystem.Shared.DataTransferModel.Auth;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Auth.Login
{
    public class LoginCommand : IRequest<ApiResponse<AuthDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
