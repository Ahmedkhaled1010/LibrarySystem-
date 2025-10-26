using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Infrastructure.Data.Context;
using LibraryManagmentSystem.Infrastructure.Data.Repositories;
using LibraryManagmentSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagmentSystem.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddIdentityCore<User>()
              .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<LibraryDbContext>();



            services.AddScoped<IServicesManager, ServicesManager>();

            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<Func<IAuthServices>>(provider => () => provider.GetService<IAuthServices>()!);
            services.AddScoped<IJwtServices, JwtServices>();
            services.AddScoped<IUserValidationServices, UserValidationServices>();



            services.AddScoped<IEmailClient, EmailClient>();
            services.AddScoped<Func<IEmailClient>>(provider => () => provider.GetService<IEmailClient>()!);
            services.AddHttpClient<IEmailClient, EmailClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7102/");
            });

            return services;
        }
    }
}
