using LibraryManagmentSystem.Application.Feature.Auth.Register;
using LibraryManagmentSystem.Application.Feature.Users.Command.ChangePassword;
using LibraryManagmentSystem.Application.Feature.Users.Command.DeleteUser;
using LibraryManagmentSystem.Application.Feature.Users.Command.NewFolder;
using LibraryManagmentSystem.Application.Feature.Users.Command.UploadProfileImage;
using LibraryManagmentSystem.Application.Feature.Users.Queries.GetUserById;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.Auth;
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
        Task<UserDto> GetUserById(string userId);
        Task<ApiResponse<string>> DeleteUserAsync(DeleteUserCommand Id);
        Task<ApiResponse<UserDto>> GetUserDetailsAsync(GetUserByIdQuery query);
        Task<ApiResponse<UserDto>> UpdateUserDetailsAsync(UpdateUserCommand query);
        Task<ApiResponse<string>> ChangePasswordAsync(ChangePasswordCommand command);
        Task<ApiResponse<string>> UploadProfileIamge(UploadProfileImageCommand command);
        Task<AuthDto> AuthUser(User user, TokenModel token,RefreshToken refreshToken);
        Task<User> CreateUserAsync(RegisterCommand command, string token);
        Task<int> GetTotalUserAsync();
    }


}
