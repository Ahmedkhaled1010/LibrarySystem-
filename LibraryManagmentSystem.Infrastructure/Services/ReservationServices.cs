using AutoMapper;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Infrastructure.Data.Specifications.ReservationSpecifications;
using LibraryManagmentSystem.Shared.DataTransferModel.Reservations;
using MassTransit;
using SharedEventsServices.Events;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class ReservationServices(IUnitOfWork unitOfWork,
        IMapper mapper,
        IServicesManager servicesManager,
        IBus bus) : IReservationServices
    {
        private readonly IGenericRepository<Reservation, Guid> genericRepository = unitOfWork.GetRepository<Reservation, Guid>();
        public async Task CreateReservation(string userId, Guid bookId)
        {
            var user = await servicesManager.UserService.GetUserById(userId);
            var book = await servicesManager.BookServices.GetBookAsync(bookId);
            Reservation reservation = new Reservation
            {
                BookId = book.Id,
                UserId = user.Id,

                ReservationDate = DateTime.Now,
            };

            var reserve = new ReserveBookEvent
            {
                BookId = book.Id,
                UserId = user.Id,
                UserName = user.Name,
                BookTitle = book.Title,
                ReservationDate = DateTime.Now,
            };

            await genericRepository.AddAsync(reservation);
            await bus.Publish(reserve);
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationAsync()
        {
            var spec = new ReservationActiveSpecification();
            var reservations = await genericRepository.GetAllAsync(spec);
            var reservationsDto = mapper.Map<IEnumerable<ReservationDto>>(reservations);
            return reservationsDto;
        }
    }
}
