using LibraryManagmentSystem.Shared.DataTransferModel.UserDto;
using LibraryManagmentSystem.Shared.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace LibraryManagmentSystem.Application.Feature.Users.Command.NewFolder
{
    public class UpdateUserCommand : IRequest<ApiResponse<UserDto>>

    {
        [JsonIgnore]
        public string? UserId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Name { get; set; }

    }
}
