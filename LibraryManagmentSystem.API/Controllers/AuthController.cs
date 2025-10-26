using LibraryManagmentSystem.Application.Feature.Auth.Login;
using LibraryManagmentSystem.Application.Feature.Auth.Register;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator, IServicesManager servicesManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginDto)
        {
            var result = await mediator.Send(loginDto);
            if (result.Success)
            {
                SetRefreshTokenInCookie(result.Data.RefreshToken!, result.Data.RefreshTokenExpiryTime);
            }
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerDto)
        {
            var result = await mediator.Send(registerDto);

            return Ok(result);
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            var result = await servicesManager.AuthServices.VerifyEmailAsync(token);
            return Ok(result);
        }
        [HttpPost("forget-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ForgetPasswordModel email)
        {
            var result = await servicesManager.AuthServices.ForgetPasswordAsync(email.Email);
            return Ok(result);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromBody] ChangePasswordModel newPassword)
        {
            var result = await servicesManager.AuthServices.ResetPasswordAsync(token, newPassword.Password);
            return Ok(result);
        }
        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return Unauthorized(new { Success = false, Message = "Refresh token is missing" });
            }

            var result = await servicesManager.AuthServices.RefreshTokenAsync(refreshToken);
            if (result.Success)
            {
                SetRefreshTokenInCookie(result.Data.RefreshToken!, result.Data.RefreshTokenExpiryTime);
            }
            return Ok(result);
        }
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenModel revokeTokenDto)
        {
            var token = revokeTokenDto.Token ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { Success = false, Message = "Token is required" });


            }
            var result = await servicesManager.AuthServices.RevokeTokenAsync(token);
            return Ok(result);
        }
        private void SetRefreshTokenInCookie(string refreshToken, DateTime expireAt)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expireAt.ToLocalTime(),
                Secure = true,
                SameSite = SameSiteMode.None
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
