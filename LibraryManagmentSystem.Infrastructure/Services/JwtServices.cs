using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class JwtServices(UserManager<User> userManager, IConfiguration configuration) : IJwtServices
    {
        public async Task<TokenModel> GenrateTokenAsync(User user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }
            var Claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id),
                    new Claim(ClaimTypes.Email,user.Email!),
                    new Claim(ClaimTypes.Name,user.UserName!),
                }.Union(userClaims).Union(roleClaims);
            var secretKey = configuration.GetSection("JWTOptions")["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: configuration.GetSection("JWTOptions")["Issuer"],
                audience: configuration.GetSection("JWTOptions")["Audience"],
                claims: Claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(configuration.GetSection("JWTOptions")["ExpiryMinutes"])),
                signingCredentials: credentials
                );
            //  return new JwtSecurityTokenHandler().WriteToken(Token);
            return new TokenModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(Token),
                Roles = roles.ToList(),
                ExpireAt = Token.ValidTo
            };
        }

        public Task<bool> ValidateUserTokenAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }
    }
}
