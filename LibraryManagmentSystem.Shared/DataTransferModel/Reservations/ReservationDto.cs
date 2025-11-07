using LibraryManagmentSystem.Domain.Enum.Reservations;

namespace LibraryManagmentSystem.Shared.DataTransferModel.Reservations
{
    public class ReservationDto
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }
        public string UserId { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now;
        public ReservationStatus Status { get; set; } = ReservationStatus.Active;
    }
}
