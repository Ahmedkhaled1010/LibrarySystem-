using LibraryManagmentSystem.Shared.DataTransferModel.Reservations;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IReservationServices
    {
        Task CreateReservation(string user, Guid book);
        Task<IEnumerable<ReservationDto>> GetAllReservationAsync();
    }
}
