using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PaymentMicroServices.Application
{
    public static class ApplicationServiceRegisteration
    {
        public static IServiceCollection AddApplicationRegisteration(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AssemblyReference));

            return services;
        }
    }
}
