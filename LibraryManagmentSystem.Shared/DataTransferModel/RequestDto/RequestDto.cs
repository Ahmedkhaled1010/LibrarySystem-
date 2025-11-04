namespace LibraryManagmentSystem.Shared.DataTransferModel.RequestDto
{
    public class RequestDto
    {
        public Guid? Id { get; set; }

        public string UserId { get; set; }
        public Guid BookId { get; set; }
        public string? BookTitle { get; set; }
        public DateTime RequestedAt { get; set; }
        public string? status { get; set; }
        public string? UserName { get; set; }


        public DateTime? returnDate { get; set; }
    }
}
