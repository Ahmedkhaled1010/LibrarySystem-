using LibraryManagmentSystem.Application.Feature.Auth.Register;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class UserValidationServices(UserManager<User> userManager) : IUserValidationServices
    {
        public async Task<string?> ValidateUserLoginAsync(User user, string loginPassword)
        {
            if (user == null || !await userManager.CheckPasswordAsync(user, loginPassword))
            {
                return "Invalid email or password";


            }
            if (!user.IsVerified)
            {
                return "Not Verifed ,Please check your email to verify your account.";
            }
            return null;
        }

        public async Task<string?> ValidateUserRegistrationAsync(RegisterCommand registerDto)
        {
            var checkEmail = await userManager.FindByEmailAsync(registerDto.EmailAddress);
            if (checkEmail != null)
                return "Email already exists";

            var domain = registerDto.EmailAddress.Split('@').Last().ToLower();
            if (domain != "gmail.com")
                return "Email domain is not allowed. Only Gmail is allowed";

            var checkUserName = await userManager.FindByNameAsync(registerDto.Name);
            if (checkUserName != null)
                return "Username already exists";

            return null;
        }
    }
}
