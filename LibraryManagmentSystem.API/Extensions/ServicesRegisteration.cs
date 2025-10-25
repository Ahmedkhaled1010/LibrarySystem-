using LibraryManagmentSystem.Shared.Error;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryManagmentSystem.API.Extensions
{
    public static class ServicesRegisteration
    {
        public static IServiceCollection AddJWTServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWTOptions:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWTOptions:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTOptions:SecretKey"])),
                    ClockSkew = TimeSpan.Zero,

                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        var response = new ValidationErrorModel
                        {
                            StatusCode = StatusCodes.Status401Unauthorized,
                            Message = "Unauthorized. Please log in to access this resource."
                        };


                        return context.Response.WriteAsJsonAsync(response);
                    },
                    OnForbidden = context =>
                    {
                        var response = new ValidationErrorModel
                        {
                            StatusCode = StatusCodes.Status403Forbidden,
                            Message = "Access denied. You don't have permission to access this resource."
                        };

                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsJsonAsync(response);
                    }

                };
            });
            return Services;
        }
    }
}
