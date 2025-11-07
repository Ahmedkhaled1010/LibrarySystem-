using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class ReservationServices(IUnitOfWork unitOfWork) : IReservationServices
    {
        private readonly IGenericRepository<Reservation, Guid> genericRepository = unitOfWork.GetRepository<Reservation, Guid>();
        public async void CreateReservation(string user, Guid book)
        {
            Reservation reservation = new Reservation
            {
                BookId = book,
                UserId = user,
                ReservationDate = DateTime.Now,
            };

            await genericRepository.AddAsync(reservation);
        }
    }
}
