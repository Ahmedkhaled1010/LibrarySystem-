using AutoMapper;
using LibraryManagmentSystem.Application.Feature.Books.Queries.GetAllBookPurchase;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Infrastructure.Data.Specifications.BookPurchaseSpecifications;
using LibraryManagmentSystem.Shared.DataTransferModel.Purchases;
using LibraryManagmentSystem.Shared.Model.BasketModule;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class BookPurchaseServices(IUnitOfWork unitOfWork, IServicesManager servicesManager,
        IMapper mapper) : IBookPurchaseServices
    {
        private readonly IGenericRepository<BookPurchase, Guid> repository = unitOfWork.GetRepository<BookPurchase, Guid>();
        public async Task AddBookPurchaseAsync(string userId, Guid BookId)
        {
            var Purchase = new BookPurchase
            {
                UserId = userId,
                BookId = BookId,
                PurchasedDate = DateTime.Now,
            };
            await repository.AddAsync(Purchase);
            var book = await servicesManager.BookServices.GetBookAsync(BookId);
            servicesManager.BookServices.UpdateAvailabilityForSaleAsync(book, false);
            try
            {
                await unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }

        }

        public async Task<IEnumerable<BookPurchaseDto>> GetAllBookPurchaseByUserIdAsync(GetAllBookPurchaseQuery userId)
        {
            var spec = new BookPurchaseSpecification(userId.userId);
            var Books = await repository.GetAllAsync(spec);
            var BooksDto = mapper.Map<IEnumerable<BookPurchaseDto>>(Books);
            return BooksDto;

        }

        public async Task GetFromBasket(CustomerBasket basket)
        {
            foreach (var item in basket.Items)
            {
                await AddBookPurchaseAsync(basket.UserId, new Guid(item.BookId));
            }
        }
    }
}
