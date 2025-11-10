namespace LibraryManagmentSystem.Shared.DataTransferModel.AdminDto
{
    public class libraryStats
    {
        public int TotalUser { get; set; }
        public int TotalBooks { get; set; }
        public int AvailableBooks { get; set; }
        public int UnAvailableBooks { get; set; }
        public int TotalBorrowed { get; set; }
        public int TotalNotReturned { get; set; }
        public decimal TotalFines { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal PendingFines { get; set; }
    }
}
