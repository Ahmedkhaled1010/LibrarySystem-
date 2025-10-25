using LibraryManagmentSystem.Application.Feature.Auth.Register;
using LibraryManagmentSystem.Domain.Entity;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IUserValidationServices
    {
        Task<string?> ValidateUserRegistrationAsync(RegisterCommand registerDto);
        Task<string?> ValidateUserLoginAsync(User user, string loginPassword);
    }
}
