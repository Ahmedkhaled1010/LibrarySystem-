using LibraryManagmentSystem.Application.IClients;
using LibraryManagmentSystem.Domain.Contracts;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IServicesManager
    {
        public IAuthServices AuthServices { get; }
        public ICategoryServices CategoryServices { get; }
        public IBookServices BookServices { get; }
        public IDocumentServices documentServices { get; }
        public IBorrowServices BorrowServices { get; }
        public IUserService UserService { get; }
        public IBorrowRecordService borrowRecordService { get; }
        public IReservationServices ReservationServices { get; }
        public IFineClient FineClient { get; }
        public IRequestClient requestClient { get; }
        public IReviewClient reviewServices { get; }
        public INotificationClient notificationClient { get; }
        public ICasheServices casheServices { get; }
        public IBasketServices basketServices { get; }
        public IBookPurchaseServices bookPurchaseServices { get; }
        public IPaymentServices paymentServices { get; }
        public IFavoriteCacheService favoriteCacheService { get; }

    }
}
