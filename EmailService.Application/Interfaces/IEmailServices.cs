using EmailService.Domain.Shared.DataTransferModel;

namespace EmailService.Application.Interfaces
{
    public interface IEmailServices
    {
        Task SendEmailAsync(EmailDto email);
    }
}
