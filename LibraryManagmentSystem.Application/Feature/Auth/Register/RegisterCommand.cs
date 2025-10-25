using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Auth.Register
{
    public class RegisterCommand : IRequest<ApiResponse<RegisterResponse>>
    {
        public string Name { get; set; } = default!;


        public string UserName { get; set; } = default!;

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; } = default!;
        public string ConfirmEmail { get; set; } = default!;


        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;



        public RegisterCommand(string name, string userName, string emailAddress, string phoneNumber, string confirmEmail, string password, string confirmPassword)
        {
            Name = name;
            UserName = userName;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            ConfirmEmail = confirmEmail;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}
