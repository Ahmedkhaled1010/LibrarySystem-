using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.Auth;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IJwtServices
    {

        Task<TokenModel> GenrateTokenAsync(User user);
        Task<bool> ValidateUserTokenAsync(string userId, string token);
    }
}
