
using LibraryManagmentSystem.Shared.Model;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IEmailClient
    {
        Task SendEmailAsync(Email email);
    }
}
