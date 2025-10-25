using LibraryManagmentSystem.Application.Feature.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
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
