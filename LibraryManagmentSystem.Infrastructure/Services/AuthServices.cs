using Google.Apis.Auth;
using LibraryManagmentSystem.Application.Feature.Auth.Login;
using LibraryManagmentSystem.Application.Feature.Auth.LoginWithGoogle;
using LibraryManagmentSystem.Application.Feature.Auth.Register;
using LibraryManagmentSystem.Application.Feature.Auth.ResetPassword;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.Auth;
using LibraryManagmentSystem.Shared.Helper;
using LibraryManagmentSystem.Shared.Response;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Cryptography;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class AuthServices(UserManager<User> userManager,
        IUserValidationServices userValidation,
        IJwtServices jwtServices,
        IBus bus,
        IOptions<AppSettings> options,
        IUnitOfWork unitOfWork,
        ILogger<AuthServices> logger,
        IServicesManager servicesManager) : IAuthServices
    {
        private readonly string baseUrl = options.Value.BaseUrl;

        public async Task<ApiResponse<RegisterResponse>> RegisterAsync(RegisterCommand registerDto)
        {

            var error = await userValidation.ValidateUserRegistrationAsync(registerDto);
            if (error != null)
            {
                return new ApiResponse<RegisterResponse>()
                {
                    Success = false,
                    Message = error,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            try
            {
                var user = await servicesManager.UserService.CreateUserAsync(registerDto, GetToken());




                if (user != null)
                {
                    await servicesManager.publishEventServices.SendEmail(user.Email, "Verify your email", $"Please verify your email by clicking on the link: {baseUrl}/api/Auth/verify-email?token={user.verificationToken}");

                    return new ApiResponse<RegisterResponse>()
                    {
                        Success = true,

                        Data = new RegisterResponse
                        {
                            Email = user.Email,
                            Name = user.UserName,


                        },
                        StatusCode = (int)HttpStatusCode.Created


                    };
                }
                else
                {
                    return ApiResponse<RegisterResponse>.Fail("Error To Register", (int)HttpStatusCode.BadRequest);

                }


            }
            catch (Exception ex)
            {
                return ApiResponse<RegisterResponse>.Fail(ex.Message);
            }
        }

        public async Task<ApiResponse<AuthDto>> LoginAsync(LoginCommand loginDto)
        {

            try
            {
                var user = await userManager.FindByEmailAsync(loginDto.Email);
                var check = await userValidation.ValidateUserLoginAsync(user, loginDto.Password);
                if (check != null)
                {
                    return new ApiResponse<AuthDto>()
                    {
                        Success = false,
                        Message = check,
                        StatusCode = (int)HttpStatusCode.BadRequest

                    };
                }
                var token = await jwtServices.GenrateTokenAsync(user);
                var User = await servicesManager.UserService.AuthUser(user, token, CreateRefreshToken());
                return new ApiResponse<AuthDto>()
                {
                    Success = true,
                    Message = "Login successful",
                    Data = User,
                    StatusCode = (int)HttpStatusCode.OK

                };
            }
            catch (Exception ex)
            {
                return ApiResponse<AuthDto>.Fail("An error occurred during login: " + ex.Message, (int)HttpStatusCode.InternalServerError);

            }
        }




        public async Task<ApiResponse<AuthDto>> RefreshTokenAsync(string token)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user is null)
            {
                return ApiResponse<AuthDto>.Fail("User Not Found", (int)HttpStatusCode.NotFound);
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
            {
                return ApiResponse<AuthDto>.Fail("Invalid Token", (int)HttpStatusCode.Unauthorized);
            }
            refreshToken.RevokedOn = DateTime.UtcNow;
            var newRefreshToken = CreateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await userManager.UpdateAsync(user);
            var jwtToken = await jwtServices.GenrateTokenAsync(user);
            var userModel = new AuthDto();

            userModel.Email = user.Email;
            userModel.IsAuthenticated = true;
            userModel.Name = user.UserName;
            userModel.Token = jwtToken.Token;
            userModel.Roles = jwtToken.Roles;
            userModel.RefreshToken = newRefreshToken.Token;
            userModel.RefreshTokenExpiryTime = newRefreshToken.ExpiresOn;
            return ApiResponse<AuthDto>.Ok(userModel, "Refresh Token Successfuly", (int)HttpStatusCode.OK);
        }

        public async Task<ApiResponse<bool>> ForgetPasswordAsync(string email)
        {

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return ApiResponse<bool>.Fail("User not found", (int)HttpStatusCode.NotFound);
            }
            var otpCode = new Random().Next(100000, 999999).ToString();

            user.resetPasswordToken = otpCode;
            user.resetPasswordTokenExpires = DateTime.UtcNow.AddMinutes(30);

            await servicesManager.publishEventServices.SendEmail(user.Email, "Reset Password Code", $"Your password reset code is: {otpCode}\nThis code will expire in 30 minutes.");

            await userManager.UpdateAsync(user);

            return ApiResponse<bool>.Ok(true, "Reset Password Email Sent");
        }
        public async Task<ApiResponse<bool>> ResetPasswordAsync(ResetPasswordCommand command)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.resetPasswordToken == command.Code && u.resetPasswordTokenExpires > DateTime.UtcNow);
            if (user == null)
            {
                return ApiResponse<bool>.Fail("Invalid or Expired Token", (int)HttpStatusCode.Unauthorized);
            }
            var resetUser = user;
            resetUser.resetPasswordToken = null;
            resetUser.resetPasswordTokenExpires = null;
            var result = await userManager.RemovePasswordAsync(resetUser);
            if (!result.Succeeded)
            {
                var Errors = result.Errors.Select(e => e.Description).ToList();
                throw new Exception(string.Join(", ", Errors));
            }
            var addPassResult = await userManager.AddPasswordAsync(resetUser, command.NewPassword);
            if (!addPassResult.Succeeded)
            {
                var Errors = addPassResult.Errors.Select(e => e.Description).ToList();
                throw new Exception(string.Join(", ", Errors));
            }
            return ApiResponse<bool>.Ok(true, "Password Reset Successfully", (int)HttpStatusCode.OK);
        }

        public async Task<ApiResponse<bool>> RevokeTokenAsync(string token)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user is null)
                return ApiResponse<bool>.Fail("User Not Found", (int)HttpStatusCode.NotFound);
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
                return ApiResponse<bool>.Fail("Invalid Token", (int)HttpStatusCode.Unauthorized);
            refreshToken.RevokedOn = DateTime.UtcNow;

            await userManager.UpdateAsync(user);
            return ApiResponse<bool>.Ok(true, "", (int)HttpStatusCode.Unauthorized);
        }

        public async Task<ApiResponse<bool>> VerifyEmailAsync(string token)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.verificationToken == token);
            if (user is null)
            {
                return ApiResponse<bool>.Fail("Invalid Token", (int)HttpStatusCode.Unauthorized);
            }
            user.IsVerified = true;
            user.verificationToken = null;
            await userManager.UpdateAsync(user);
            return ApiResponse<bool>.Ok(true, "Email Verified Successfully", (int)HttpStatusCode.OK);
        }
        private string GetToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            var token = Convert.ToBase64String(randomNumber);
            token = token.Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");

            return token;

        }
        private RefreshToken CreateRefreshToken()
        {
            return new RefreshToken
            {
                Token = GetToken(),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow
            };
        }

        public async Task<ApiResponse<bool>> GoogleSignInAsync(LoginWithGoogleCommand command)
        {
            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(command.token);

            }
            catch
            {
                return ApiResponse<bool>.Fail("Invalid Google Token");
            }
            var email = payload.Email;
            var name = payload.Name;
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    Name = name,
                };

                userManager.CreateAsync(user);
                return ApiResponse<bool>.Ok(true);
            }

            return ApiResponse<bool>.Fail("Email already exists");

        }
    }
}
