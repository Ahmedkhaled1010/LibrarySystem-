namespace LibraryManagmentSystem.Shared.Response
{
    public class RegisterResponse
    {
        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
        public bool IsVerified { get; set; } = false;
        public string Message { get; set; } = "Please check your email to verify your account.";
    }
}
