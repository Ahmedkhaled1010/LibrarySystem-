namespace LibraryManagmentSystem.Shared.DataTransferModel.Review
{
    public class BookRatingAvgDto
    {
        public Guid BookId { get; set; }
        public double AvgRating { get; set; }
    }
}
