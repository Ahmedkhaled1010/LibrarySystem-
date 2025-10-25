using EmailService.Application.Helper;
using EmailService.Application.Interfaces;
using EmailService.Domain.Contracts;
using EmailService.Domain.Models;
using EmailService.Domain.Shared.DataTransferModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace EmailService.Infrastructure.Services
{
    public class EmailServices : IEmailServices

    {
        private readonly EmailSettings _options;
        private readonly ILogger<EmailServices> logger;
        private readonly IEmailRepository repository;

        public EmailServices(IOptions<EmailSettings> options,
            ILogger<EmailServices> logger,
            IEmailRepository repository)
        {
            _options = options.Value;
            this.logger = logger;
            this.repository = repository;
        }
        public async Task SendEmailAsync(EmailDto email)
        {

            using var Client = new SmtpClient(_options.Host, _options.Port)
            {
                Credentials = new NetworkCredential(_options.From, _options.Password),
                EnableSsl = true
            };
            var Message = new MailMessage
            {
                From = new MailAddress(_options.From, "Support Team"),

                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true,
            };
            var emailModel = new Email
            {
                To = email.To,
                Subject = email.Subject,
                Body = email.Body,
            };
            Message.To.Add(new MailAddress(email.To!));
            try
            {
                await Client.SendMailAsync(Message);
                await repository.AddEmailAsync(emailModel);
            }
            catch (SmtpException ex)
            {
                logger.LogError($"SMTP Error: {ex.StatusCode} - {ex.Message}");
                throw new Exception($"Failed to send email: {ex.Message}", ex);
            }
        }
    }
}
