using LibraryManagmentSystem.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace LibraryManagmentSystem.Application.Feature.Users.Command.UploadProfileImage
{
    public class UploadProfileImageCommand : IRequest<ApiResponse<string>>
    {
        public IFormFile file { get; set; }
        [JsonIgnore]


        public string? UserId { get; set; }
    }
}
