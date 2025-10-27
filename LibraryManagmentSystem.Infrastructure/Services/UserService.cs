using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.Response;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class UserService(UserManager<User> userManager) : IUserService
    {
        public async Task UpdateBorrowLimitAsync(User user, int change)
        {
            user.LimitOfBooksCanBorrow += change;
            await userManager.UpdateAsync(user);
        }

        public async Task<ApiResponse<string>> ValidateUserForBorrowing(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return ApiResponse<string>.Fail("User not found");
            }
            if (user.fines > 0)
            {
                return ApiResponse<string>.Fail("You cannot borrow before paying the fines.");
            }
            if (user.LimitOfBooksCanBorrow <= 0)
            {
                return ApiResponse<string>.Fail("You have exceeded your allowed limit, upgrade your Budget");
            }
            return null;
        }
    }
}
