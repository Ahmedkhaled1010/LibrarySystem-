using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace LibraryManagmentSystem.Domain.Entity
{
    public class Book : BaseEntity<Guid>
    {
        public string Title { get; set; } = default!;
        public int PublishedYear { get; set; }
        public int CopiesAvailable { get; set; }
        public bool IsAvailable => CopiesAvailable > 0;
        [ForeignKey(nameof(Author))]

        public string AuthorId { get; set; } = default!;

        [InverseProperty("BooksAuthored")]
        public User Author { get; set; } = default!;

        public List<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();

        public int BorrowDurationDays { get; set; } = 10;

        public long Price { get; set; }


        public Guid? PdfId { get; set; }
        public Document? Pdf { get; set; }
        public Guid? CoverImageId { get; set; }
        public Document? CoverImage { get; set; }

        public int TotalSell { get; set; }
        public int TotalBorrow { get; set; }
        ICollection<OrderBook> orders { get; set; }
    }
}
