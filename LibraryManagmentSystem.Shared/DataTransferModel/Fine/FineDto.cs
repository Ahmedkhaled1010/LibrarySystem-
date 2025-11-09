namespace LibraryManagmentSystem.Shared.DataTransferModel.Fine
{
    public class FineDto
    {
        public string? Id { get; set; }
        public string UserId { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Reason { get; set; } = default!;
        public string BorrowId { get; set; } = default!;
        public DateTime DateIssued { get; set; }

        public bool IsPaid { get; set; }
    }
}
