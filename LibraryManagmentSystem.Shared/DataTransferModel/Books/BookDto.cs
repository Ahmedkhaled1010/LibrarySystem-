namespace LibraryManagmentSystem.Shared.DataTransferModel.Books
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public bool IsAvailableForSale { get; set; }
        public bool IsAvailable { get; set; }
        public int CopiesAvailable { get; set; } = 1;
        public int CopiesForSaleAvailable { get; set; }
        public string Description { get; set; } = default!;

        public string CategoryName { get; set; } = default!;
        public int PublishedYear { get; set; }
        public int BorrowDurationDays { get; set; } = 10;
        public long Price { get; set; }
        public string AuthorName { get; set; } = default!;

        public string Language { get; set; } = default!;
        public string? PdfUrl { get; set; }
        public string? CoverImageUrl { get; set; }
        public int Pages { get; set; }
        public string? Status { get; set; }
        public int TotalSell { get; set; }
        public int TotalBorrow { get; set; }


    }
}
