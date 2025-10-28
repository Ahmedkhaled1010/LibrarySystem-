using LibraryManagmentSystem.Shared.Model;

namespace LibraryManagmentSystem.Application.IClients
{
    public interface IEmailClient
    {
        Task SendEmailAsync(Email email);
    }
}
