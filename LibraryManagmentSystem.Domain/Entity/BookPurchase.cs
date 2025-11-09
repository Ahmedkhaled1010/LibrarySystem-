namespace LibraryManagmentSystem.Domain.Entity
{
    public class BookPurchase : BaseEntity<Guid>
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public Guid BookId { get; set; }
        public Book Book { get; set; }

        public DateTime PurchasedDate { get; set; } = DateTime.UtcNow;



    }
}
