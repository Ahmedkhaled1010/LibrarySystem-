
using EmailService.Application.Helper;
using EmailService.Application.Interfaces;
using EmailService.Domain.Contracts;
using EmailService.Infrastructure.Data.Context;
using EmailService.Infrastructure.Data.Repositories;
using EmailService.Infrastructure.Services;

namespace EmailService
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
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IEmailServices, EmailServices>();
            builder.Services.Configure<EmailSettings>(
               builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddSingleton<MongoDb>();
            builder.Services.AddSingleton(typeof(IEmailRepository), typeof(EmailRepository));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
