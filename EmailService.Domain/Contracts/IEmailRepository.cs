using EmailService.Domain.Models;

namespace EmailService.Domain.Contracts
{
    public interface IEmailRepository
    {
        Task<Email> GetEmailByIdAsync(string id);
        Task<IEnumerable<Email>> GetAllEmailsAsync();
        Task AddEmailAsync(Email entity);
        Task UpdateEmailsync(string id, Email entity);
        Task DeleteEmailsync(string id);
    }
}
