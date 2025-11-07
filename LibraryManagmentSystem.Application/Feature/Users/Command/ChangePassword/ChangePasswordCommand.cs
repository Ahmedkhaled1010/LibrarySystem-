using LibraryManagmentSystem.Shared.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace LibraryManagmentSystem.Application.Feature.Users.Command.ChangePassword
{
    public class ChangePasswordCommand : IRequest<ApiResponse<string>>
    {
        [JsonIgnore]
        public string? UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
