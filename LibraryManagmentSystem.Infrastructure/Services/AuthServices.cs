using LibraryManagmentSystem.Application.Feature.Auth.Login;
using LibraryManagmentSystem.Application.Feature.Auth.Register;
using LibraryManagmentSystem.Application.Feature.Auth.ResetPassword;
using LibraryManagmentSystem.Application.IClients;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.Auth;
using LibraryManagmentSystem.Shared.Helper;
using LibraryManagmentSystem.Shared.Response;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedEventsServices.Events;
using System.Security.Cryptography;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class AuthServices(UserManager<User> userManager,
        IUserValidationServices userValidation,
        IJwtServices jwtServices,
        IEmailClient emailClient,
        IBus bus,
        IOptions<AppSettings> options) : IAuthServices
    {
        private readonly string baseUrl = options.Value.BaseUrl;

        public async Task<ApiResponse<RegisterResponse>> RegisterAsync(RegisterCommand registerDto)
        {
            User user = new User
            {
                Email = registerDto.EmailAddress,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
                Name = registerDto.Name
            };
            var error = await userValidation.ValidateUserRegistrationAsync(registerDto);
            if (error != null)
            {
                return new ApiResponse<RegisterResponse>()
                {
                    Success = false,
                    Message = error
                };
            }
            user.verificationToken = GetToken();
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                await userManager.UpdateAsync(user);



                var mail = new SendEmailEvent()
                {
                    To = user.Email,
                    Subject = "Verify your email",
                    Body = $"Please verify your email by clicking on the link: {baseUrl}/api/Auth/verify-email?token={user.verificationToken}"
                };
                await bus.Publish<SendEmailEvent>(mail);
                return new ApiResponse<RegisterResponse>()
                {
                    Success = true,

                    Data = new RegisterResponse
                    {
                        Email = user.Email,
                        Name = user.UserName,

                    }

                };
            }
            else
            {
                var Errors = result.Errors.Select(e => e.Description).ToList();
                //  throw new BadRequestException(Errors);
                throw new Exception(string.Join(", ", Errors));
            }
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




        public async Task<ApiResponse<AuthDto>> RefreshTokenAsync(string token)
        {
            var userModel = new AuthDto();
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user is null)
            {
                return ApiResponse<AuthDto>.Fail("User Not Found");
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
            {
                return ApiResponse<AuthDto>.Fail("Invalid Token");
            }
            refreshToken.RevokedOn = DateTime.UtcNow;
            var newRefreshToken = new RefreshToken()
            {
                Token = GetToken(),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow,
            };
            user.RefreshTokens.Add(newRefreshToken);
            await userManager.UpdateAsync(user);
            var jwtToken = await jwtServices.GenrateTokenAsync(user);
            userModel.Email = user.Email;
            userModel.IsAuthenticated = true;
            userModel.Name = user.UserName;
            userModel.Token = jwtToken.Token;
            userModel.Roles = jwtToken.Roles;
            userModel.RefreshToken = newRefreshToken.Token;
            userModel.RefreshTokenExpiryTime = newRefreshToken.ExpiresOn;
            return ApiResponse<AuthDto>.Ok(userModel, "Refresh Token Successfuly");
        }

        public async Task<ApiResponse<bool>> ForgetPasswordAsync(string email)
        {

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return ApiResponse<bool>.Fail("User not found");
            }
            var otpCode = new Random().Next(100000, 999999).ToString();

            user.resetPasswordToken = otpCode;
            user.resetPasswordTokenExpires = DateTime.UtcNow.AddMinutes(30);

            //var mail = new Email()
            //{
            //    To = user.Email,
            //    Body = $"Your password reset code is: {otpCode}\nThis code will expire in 30 minutes.",
            //    Subject = "Reset Password Code"
            //};
            ////  emailServices.SendEmail(mail);
            //await emailClient.SendEmailAsync(mail);
            var mail = new SendEmailEvent()
            {
                To = user.Email,
                Body = $"Your password reset code is: {otpCode}\nThis code will expire in 30 minutes.",
                Subject = "Reset Password Code"
            };
            await bus.Publish<SendEmailEvent>(mail);
            await userManager.UpdateAsync(user);

            return ApiResponse<bool>.Ok(true, "Reset Password Email Sent");
        }
        public async Task<ApiResponse<bool>> ResetPasswordAsync(ResetPasswordCommand command)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.resetPasswordToken == command.Code && u.resetPasswordTokenExpires > DateTime.UtcNow);
            if (user == null)
            {
                return ApiResponse<bool>.Fail("Invalid or Expired Token");
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
            return ApiResponse<bool>.Ok(true, "Password Reset Successfully");
        }

        public async Task<ApiResponse<bool>> RevokeTokenAsync(string token)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user is null)
                return ApiResponse<bool>.Fail("User Not Found");
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
                return ApiResponse<bool>.Fail("Invalid Token");
            refreshToken.RevokedOn = DateTime.UtcNow;

            await userManager.UpdateAsync(user);
            return ApiResponse<bool>.Ok(true, "");
        }

        public async Task<ApiResponse<bool>> VerifyEmailAsync(string token)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.verificationToken == token);
            if (user is null)
            {
                return ApiResponse<bool>.Fail("Invalid Token");
            }
            user.IsVerified = true;
            user.verificationToken = null;
            await userManager.UpdateAsync(user);
            return ApiResponse<bool>.Ok(true, "Email Verified Successfully");
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
    }
}
