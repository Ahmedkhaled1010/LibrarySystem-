using LibraryManagmentSystem.Application.Feature.Auth.Login;
using LibraryManagmentSystem.Application.Feature.Auth.Register;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.Auth;
using LibraryManagmentSystem.Shared.Response;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class AuthServices(UserManager<User> userManager,
        IUserValidationServices userValidation,
        IJwtServices jwtServices) : IAuthServices
    {
        public Task<ApiResponse<RegisterResponse>> RegisterAsync(RegisterCommand user)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<AuthDto>> LoginAsync(LoginCommand loginDto)
        {

            var user = await userManager.FindByEmailAsync(loginDto.Email);
            var check = await userValidation.ValidateUserLoginAsync(user, loginDto.Password);
            if (check != null)
            {
                return new ApiResponse<AuthDto>()
                {
                    Success = false,
                    Message = check
                };
            }
            var token = await jwtServices.GenrateTokenAsync(user);
            var User = new AuthDto()
            {
                Email = user.Email,
                IsAuthenticated = true,
                Name = user.UserName,
                Token = token.Token,
                Roles = token.Roles,
                IsVerified = user.IsVerified
            };
            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var refreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                User.RefreshToken = refreshToken.Token;
                User.RefreshTokenExpiryTime = refreshToken.ExpiresOn;


            }
            else
            {

                var refreshToken = new RefreshToken()
                {
                    Token = GetToken(),
                    ExpiresOn = DateTime.UtcNow.AddDays(10),
                    CreatedOn = DateTime.UtcNow,
                };
                User.RefreshToken = refreshToken.Token;
                User.RefreshTokenExpiryTime = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                await userManager.UpdateAsync(user);
            }
            return new ApiResponse<AuthDto>()
            {
                Success = true,
                Message = "Login successful",
                Data = User
            };
        }

        public Task<ApiResponse<bool>> ForgetPasswordAsync(string token)
        {
            throw new NotImplementedException();
        }


        public Task<ApiResponse<AuthDto>> RefreshTokenAsync(string token)
        {
            throw new NotImplementedException();
        }


        public Task<ApiResponse<bool>> ResetPasswordAsync(string email, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> RevokeTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> VerifyEmailAsync(string token)
        {
            throw new NotImplementedException();
        }
        private string GetToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);

        }
    }
}
