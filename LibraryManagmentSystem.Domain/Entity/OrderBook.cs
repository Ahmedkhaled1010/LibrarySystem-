using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagmentSystem.Domain.Entity
{
    public class OrderBook : BaseEntity<Guid>
    {

        public DateTime? OrderBookDate { get; set; } = DateTime.Now;
        public decimal Price { get; set; }
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; } = default!;


        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; } = default!;


    }
}
