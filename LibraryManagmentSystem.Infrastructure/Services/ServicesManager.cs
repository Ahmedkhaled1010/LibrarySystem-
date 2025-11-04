using LibraryManagmentSystem.Application.IClients;
using LibraryManagmentSystem.Application.Interfaces;

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
        Func<IReviewClient> IReview

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
    }
}
