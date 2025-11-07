using AutoMapper;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Infrastructure.Data.Specifications.ReservationSpecifications;
using LibraryManagmentSystem.Shared.DataTransferModel.Reservations;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class ReservationServices(IUnitOfWork unitOfWork, IMapper mapper) : IReservationServices
    {
        private readonly IGenericRepository<Reservation, Guid> genericRepository = unitOfWork.GetRepository<Reservation, Guid>();
        public async Task CreateReservation(string user, Guid book)
        {
            Reservation reservation = new Reservation
            {
                BookId = book,
                UserId = user,
                ReservationDate = DateTime.Now,
            };

            await genericRepository.AddAsync(reservation);
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
