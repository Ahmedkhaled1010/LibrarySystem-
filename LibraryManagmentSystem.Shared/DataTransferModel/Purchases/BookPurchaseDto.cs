using LibraryManagmentSystem.Shared.DataTransferModel.Books;

namespace LibraryManagmentSystem.Shared.DataTransferModel.Purchases
{
    public class BookPurchaseDto
    {
        public string UserId { get; set; }

        public BookDto Book { get; set; }

        public DateTime PurchasedDate { get; set; } = DateTime.UtcNow;

    }
}
