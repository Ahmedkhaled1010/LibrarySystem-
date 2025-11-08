using LibraryManagmentSystem.Shared.DataTransferModel.Books;

namespace LibraryManagmentSystem.Shared.DataTransferModel.BasketDto
{
    public class BasketItemDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string BookId { get; set; } = default!;
        public BookDto book { get; set; }
        public DateTime DateAdded { get; set; }
        public int Quantity { get; set; }
    }
}
