namespace LibraryManagmentSystem.Shared.DataTransferModel.Review
{
    public class ReviewDto
    {
        public Guid? Id { get; set; }

        public string? Comment { get; set; }
        public string? BookTitle { get; set; }
        public string? UserName { get; set; }
        public Guid? BookId { get; set; }
        public DateTime? Created { get; set; }
        public int? Rating { get; set; }
    }
}
