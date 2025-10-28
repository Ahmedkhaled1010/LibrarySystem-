using Microsoft.Extensions.DependencyInjection;
using PaymentMicroServices.Application.Interfaces;
using PaymentMicroServices.Domain.Contracts;
using PaymentMicroServices.InfraStructure.Data.Context;
using PaymentMicroServices.InfraStructure.Data.Repositories;
using PaymentMicroServices.InfraStructure.Services;

namespace PaymentMicroServices.InfraStructure
{
    public static class InfraStructureRegisteration
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cf => { }, typeof(AssemblyReference).Assembly);

            services.AddSingleton<MongoDb>();
            services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IServicesManager, ServicesManager>();
            services.AddScoped<IFineServices, FineServices>();
            services.AddScoped<Func<IFineServices>>(provider => () => provider.GetService<IFineServices>()!);


            return services;
        }
    }
}
