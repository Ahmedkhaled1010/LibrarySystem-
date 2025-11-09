using LibraryManagmentSystem.Application.IClients;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Infrastructure.Clients;
using LibraryManagmentSystem.Infrastructure.Data.Context;
using LibraryManagmentSystem.Infrastructure.Data.MongoContext;
using LibraryManagmentSystem.Infrastructure.Data.Repositories;
using LibraryManagmentSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

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
            services.AddSingleton<MongoDb>();
            services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnectionString"));
            });
            services.AddScoped<IServicesManager, ServicesManager>();

            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<Func<IAuthServices>>(provider => () => provider.GetService<IAuthServices>()!);
            services.AddScoped<IJwtServices, JwtServices>();
            services.AddScoped<IUserValidationServices, UserValidationServices>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<Func<ICategoryServices>>(provider => () => provider.GetService<ICategoryServices>()!);
            services.AddScoped<IBookServices, BookServices>();
            services.AddScoped<Func<IBookServices>>(provider => () => provider.GetService<IBookServices>()!);
            services.AddScoped<IDocumentServices, DocumentServices>();
            services.AddScoped<Func<IDocumentServices>>(provider => () => provider.GetService<IDocumentServices>()!);
            services.AddScoped<IBorrowServices, BorrowServices>();
            services.AddScoped<Func<IBorrowServices>>(provider => () => provider.GetService<IBorrowServices>()!);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<Func<IUserService>>(provider => () => provider.GetService<IUserService>()!);
            services.AddScoped<IBorrowRecordService, BorrowRecordService>();
            services.AddScoped<Func<IBorrowRecordService>>(provider => () => provider.GetService<IBorrowRecordService>()!);
            services.AddScoped<IReservationServices, ReservationServices>();
            services.AddScoped<Func<IReservationServices>>(provider => () => provider.GetService<IReservationServices>()!); services.AddScoped<IReviewClient, ReviewClient>();


            services.AddScoped<Func<IReviewClient>>(provider => () => provider.GetService<IReviewClient>()!);

            services.AddScoped<ICasheServices, CasheServices>();
            services.AddScoped<Func<ICasheServices>>(provider => () => provider.GetService<ICasheServices>()!);
            services.AddScoped<IFavoriteCacheService, FavoriteCacheService>();
            services.AddScoped<Func<IFavoriteCacheService>>(provider => () => provider.GetService<IFavoriteCacheService>()!);
            services.AddScoped<IBasketServices, BasketServices>();
            services.AddScoped<Func<IBasketServices>>(provider => () => provider.GetService<IBasketServices>()!);
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped<Func<IPaymentServices>>(provider => () => provider.GetService<IPaymentServices>()!);
            services.AddScoped<IBookPurchaseServices, BookPurchaseServices>();
            services.AddScoped<Func<IBookPurchaseServices>>(provider => () => provider.GetService<IBookPurchaseServices>()!);
            services.AddScoped<IRequestClient, RequestClient>();
            services.AddScoped<Func<IRequestClient>>(provider => () => provider.GetService<IRequestClient>()!);
            services.AddScoped<INotificationClient, NotificationClient>();
            services.AddScoped<Func<INotificationClient>>(provider => () => provider.GetService<INotificationClient>()!);
            services.AddScoped<ISupabaseClient, SupabaseClient>();

            services.AddScoped<IFineClient, FineClient>();
            services.AddScoped<Func<IFineClient>>(provider => () => provider.GetService<IFineClient>()!);
            services.AddHttpClient<IFineClient, FineClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7207/");
            });

            return services;
        }
    }
}
