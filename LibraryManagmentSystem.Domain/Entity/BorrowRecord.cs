using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagmentSystem.Domain.Entity
{
    public class BorrowRecord : BaseEntity<Guid>
    {
        public DateTime BorrowDate { get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }

        [ForeignKey(nameof(Book))]
        public Guid BookId { get; set; }
        public Book Book { get; set; } = default!;

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; } = default!;

        public void SetActualReturnDate(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            ActualReturnDate = BorrowDate.AddDays(book.BorrowDurationDays);
        }
    }
}
