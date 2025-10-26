namespace LibraryManagmentSystem.Shared.DataTransferModel.Books
{
    public class BookDto
    {
        public string Title { get; set; } = default!;


        public int CopiesAvailable { get; set; } = 1;

        public string CategoryName { get; set; } = default!;
        public int PublishedYear { get; set; }
        public int BorrowDurationDays { get; set; } = 10;
        public long Price { get; set; }
        public string AuthorId { get; set; } = default!;
        public string AuthorName { get; set; } = default!;
    }
}
