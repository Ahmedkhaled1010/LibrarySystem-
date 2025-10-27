using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<string>> ValidateUserForBorrowing(string userId);
        Task UpdateBorrowLimitAsync(User user, int change);
    }
}
