using LibraryManagmentSystem.Application.IClients;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class ServicesManager(Func<IAuthServices> IAuth,
        Func<ICategoryServices> ICategory,
        Func<IBookServices> IBook,
        Func<IDocumentServices> IDocument,
        Func<IBorrowServices> IBorrow,
        Func<IUserService> IUser,
        Func<IBorrowRecordService> IBorrowRecord,
        Func<IFineClient> IFine,
        Func<IRequestClient> IRequest,
        Func<IReviewClient> IReview,
        Func<INotificationClient> INotify,
        Func<IReservationServices> IReserve,
        Func<ICasheServices> ICashe,
        Func<IBasketServices> IBasket,
        Func<IPaymentServices> IPayment,
        Func<IBookPurchaseServices> IPurchase,
        Func<IFavoriteCacheService> IFav,
        Func<IAdminServices> IAdmin,
        Func<IPublishEventServices> IPublish
       ) : IServicesManager
    {
        public IAuthServices AuthServices => IAuth.Invoke();

        public ICategoryServices CategoryServices => ICategory.Invoke();

        public IBookServices BookServices => IBook.Invoke();

        public IDocumentServices documentServices => IDocument.Invoke();

        public IBorrowServices BorrowServices => IBorrow.Invoke();

        public IUserService UserService => IUser.Invoke();

        public IBorrowRecordService borrowRecordService => IBorrowRecord.Invoke();

        public IFineClient FineClient => IFine.Invoke();

        public IRequestClient requestClient => IRequest.Invoke();

        public IReviewClient reviewServices => IReview.Invoke();

        public INotificationClient notificationClient => INotify.Invoke();

        public IReservationServices ReservationServices => IReserve.Invoke();

        public ICasheServices casheServices => ICashe.Invoke();

        public IBasketServices basketServices => IBasket.Invoke();

        public IBookPurchaseServices bookPurchaseServices => IPurchase.Invoke();

        public IPaymentServices paymentServices => IPayment.Invoke();

        public IFavoriteCacheService favoriteCacheService => IFav.Invoke();

        public IAdminServices adminServices => IAdmin.Invoke();

        public IPublishEventServices publishEventServices => IPublish.Invoke();
    }
}
