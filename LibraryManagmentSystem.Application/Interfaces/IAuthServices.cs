using LibraryManagmentSystem.Application.Feature.Auth.Login;
using LibraryManagmentSystem.Application.Feature.Auth.LoginWithGoogle;
using LibraryManagmentSystem.Application.Feature.Auth.Register;
using LibraryManagmentSystem.Application.Feature.Auth.ResetPassword;
using LibraryManagmentSystem.Shared.DataTransferModel.Auth;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IAuthServices
    {

        Task<ApiResponse<RegisterResponse>> RegisterAsync(RegisterCommand user);
        Task<ApiResponse<AuthDto>> LoginAsync(LoginCommand user);
        Task<ApiResponse<AuthDto>> RefreshTokenAsync(string token);
        Task<ApiResponse<bool>> RevokeTokenAsync(string token);
        Task<ApiResponse<bool>> VerifyEmailAsync(string token);
        Task<ApiResponse<bool>> ForgetPasswordAsync(string token);
        Task<ApiResponse<bool>> ResetPasswordAsync(ResetPasswordCommand command);
        Task<ApiResponse<bool>> GoogleSignInAsync(LoginWithGoogleCommand command);


    }
}
