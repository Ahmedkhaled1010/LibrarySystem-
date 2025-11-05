namespace LibraryManagmentSystem.Shared.DataTransferModel.UserDto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double fines { get; set; }
        public string PhoneNumber { get; set; }
        public int TotalBuy { get; set; }
        public int TotalBorrow { get; set; }
        public decimal invoice { get; set; }
        public string Role { get; set; }
    }
}
