using LibraryManagmentSystem.Application.IClients;

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
        public IEmailClient EmailClient { get; }
        public IFineClient FineClient { get; }

    }
}
