namespace LibraryManagmentSystem.Shared.Model
{
    public class Fine
    {
        public string UserId { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Reason { get; set; } = default!;
        public DateTime DateIssued { get; set; }
    }
}
