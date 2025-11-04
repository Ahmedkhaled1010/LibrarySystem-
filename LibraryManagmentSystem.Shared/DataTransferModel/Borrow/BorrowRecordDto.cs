using LibraryManagmentSystem.Domain.Enum.BorrowRecord;
using LibraryManagmentSystem.Shared.DataTransferModel.Books;

namespace LibraryManagmentSystem.Shared.DataTransferModel.Borrow
{
    public class BorrowRecordDto
    {
        public string Id { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public BookDto Book { get; set; }
        public string Status { get; set; } = BorrowRecordStatus.active.ToString();


    }
}
