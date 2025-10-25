using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagmentSystem.Application
{
    public static class ApplicationServiceRegisteration
    {
        public static IServiceCollection AddApplicationRegisteration(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AssemblyReference));
            services.AddAutoMapper(cf => { }, typeof(AssemblyReference).Assembly);

            return services;
        }
    }
}
