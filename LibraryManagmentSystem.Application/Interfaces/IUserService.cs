using LibraryManagmentSystem.Application.Feature.Users.Command.ChangePassword;
using LibraryManagmentSystem.Application.Feature.Users.Command.NewFolder;
using LibraryManagmentSystem.Application.Feature.Users.Queries.GetUserById;
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
        Task<ApiResponse<UserDto>> GetUserDetailsAsync(GetUserByIdQuery query);
        Task<ApiResponse<UserDto>> UpdateUserDetailsAsync(UpdateUserCommand query);
        Task<ApiResponse<string>> ChangePasswordAsync(ChangePasswordCommand command);
    }


}
