using LibraryManagmentSystem.Application.Feature.Borrow.Command.BorrowBook;
using LibraryManagmentSystem.Application.Feature.Borrow.Command.ResponsBorrowBook;
using LibraryManagmentSystem.Application.Feature.Borrow.Command.ReturnBook;
using LibraryManagmentSystem.Application.Feature.Borrow.Queries.GetAllBorrowByUser;
using LibraryManagmentSystem.Shared.DataTransferModel.Borrow;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IBorrowServices
    {
        public Task<ApiResponse<string>> RequestBorrowBook(BorrowBookCommand command);
        public Task<ApiResponse<string>> ReturnBook(ReturnBookCommand bookCommand);
        public Task<ApiResponse<string>> BorrowBook(string UserId, Guid BookId);
        public Task<ApiResponse<string>> ApproveBorrowRequest(ResponsBorrowBookCommand command);
        Task<ApiResponse<IEnumerable<BorrowRecordDto>>> BorrowingHistory(GetAllBorrowByUserQuery user);



    }
}
