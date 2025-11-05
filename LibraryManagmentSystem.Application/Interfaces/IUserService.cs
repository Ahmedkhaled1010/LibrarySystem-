using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.UserDto;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<string>> ValidateUserForBorrowing(string userId);
        Task UpdateBorrowLimitAsync(User user, int change);
        Task<ApiResponse<IEnumerable<UserDto>>> GetAllUser();
        Task UpdateTotalBorrowAsync(User user);
    }


}
