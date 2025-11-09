using LibraryManagmentSystem.Shared.Enum.Book;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagmentSystem.Domain.Entity
{
    public class Book : BaseEntity<Guid>
    {
        public string Title { get; set; } = default!;
        public int PublishedYear { get; set; }
        //public int CopiesAvailable { get; set; }
        //public int CopiesForSaleAvailable { get; set; }
        //public bool IsAvailableForSale => CopiesForSaleAvailable > 0;

        //public bool IsAvailable => CopiesAvailable > 0;
        public bool IsAvailableForSale { get; set; } = true;

        public bool IsAvailable { get; set; } = true;
        [ForeignKey(nameof(Author))]

        public string AuthorId { get; set; } = default!;

        [InverseProperty("BooksAuthored")]
        public User Author { get; set; } = default!;

        public List<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();

        public int BorrowDurationDays { get; set; } = 10;

        public long Price { get; set; }
        [ForeignKey(nameof(Category))]

        public Guid CategoryId { get; set; } = default!;

        public Category Category { get; set; } = default!;

        public Guid? PdfId { get; set; }
        public Guid? CoverImageId { get; set; }
        public string? PdfUrl { get; set; }
        public string? CoverImageUrl { get; set; }
        public string Description { get; set; } = default!;
        public int TotalSell { get; set; }
        public int TotalBorrow { get; set; }
        public string Language { get; set; } = default!;
        public int Pages { get; set; }
        ICollection<OrderBook> orders { get; set; }
        public string Status { get; set; } = BookStatus.Available.ToString();
    }
}
