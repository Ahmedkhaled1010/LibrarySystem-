namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IReservationServices
    {
        void CreateReservation(string user, Guid book);
    }
}
