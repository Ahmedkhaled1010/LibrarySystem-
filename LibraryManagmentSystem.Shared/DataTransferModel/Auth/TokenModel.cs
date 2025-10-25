namespace LibraryManagmentSystem.Shared.DataTransferModel.Auth
{
    public class TokenModel
    {
        public string Token { get; set; } = default!;
        public DateTime ExpireAt { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
