using System.Text.Json.Serialization;

namespace LibraryManagmentSystem.Shared.DataTransferModel.Auth
{
    public class AuthDto
    {
        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
        public bool IsVerified { get; set; } = false;

        public bool IsAuthenticated { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public string Token { get; set; } = default!;
        [JsonIgnore]
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
