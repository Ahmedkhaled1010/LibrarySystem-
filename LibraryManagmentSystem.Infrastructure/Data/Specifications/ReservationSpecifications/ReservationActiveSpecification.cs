using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Domain.Enum.Reservations;

namespace LibraryManagmentSystem.Infrastructure.Data.Specifications.ReservationSpecifications
{
    class ReservationActiveSpecification : BaseSpecification<Reservation, Guid>
    {
        public ReservationActiveSpecification() : base(r => r.Status == ReservationStatus.Active)
        {
            AddInclude(r => r.User);
        }
    }
}
