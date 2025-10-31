using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Auth.ResetPassword
{
    public class ResetPasswordCommand : IRequest<ApiResponse<bool>>
    {
        public string Code { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
        public string ConfirmNewPassword { get; set; } = default!;
    }
}
