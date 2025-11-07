using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.Domain.Entity
{
    public class User : IdentityUser
    {

        public string Name { get; set; } = default!;
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        public List<Book> BooksAuthored { get; set; } = new List<Book>();
        public List<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();

        public List<RefreshToken>? RefreshTokens { get; set; }
        public string? AccessToken { get; set; }
        public int LimitOfBooksCanBorrow { get; set; } = 3;

        //  public string budget { get; set; } = Budget.FREE.ToString();
        public double fines { get; set; } = 0.0;

        public ICollection<OrderBook> Orders { get; set; } = new List<OrderBook>();
        public int TotalBuy { get; set; }
        public int TotalBorrow { get; set; }
        public decimal invoice { get; set; }
        public bool IsVerified { get; set; } = false;

        public string? verificationToken { get; set; }
        public string? resetPasswordToken { get; set; }
        public DateTime? resetPasswordTokenExpires { get; set; }
        public string? ImagePath { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
