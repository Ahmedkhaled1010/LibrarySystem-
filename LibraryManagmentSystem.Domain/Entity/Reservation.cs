using LibraryManagmentSystem.Domain.Enum.Reservations;

namespace LibraryManagmentSystem.Domain.Entity
{
    public class Reservation : BaseEntity<Guid>
    {

        public Guid BookId { get; set; }
        public string UserId { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now;
        public ReservationStatus Status { get; set; } = ReservationStatus.Active;
    }
}
