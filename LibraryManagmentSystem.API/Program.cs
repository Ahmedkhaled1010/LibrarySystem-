
using LibraryManagmentSystem.API.Extensions;
using LibraryManagmentSystem.API.Middlewares;
using LibraryManagmentSystem.Application;
using LibraryManagmentSystem.Infrastructure;
using LibraryManagmentSystem.Shared.Helper;

namespace LibraryManagmentSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationRegisteration();
            builder.Services.AddJWTServices(builder.Configuration);
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.EnableFilter();
                    options.EnablePersistAuthorization();
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
