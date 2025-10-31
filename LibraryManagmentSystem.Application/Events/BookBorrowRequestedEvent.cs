namespace LibraryManagmentSystem.Application.Events
{
    public class BookBorrowRequestedEvent
    {
        public Guid RequestId { get; set; }
        public string UserId { get; set; }
        public Guid BookId { get; set; }
        public string? BookTitle { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
