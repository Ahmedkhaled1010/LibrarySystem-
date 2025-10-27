using LibraryManagmentSystem.Application.Feature.Borrow.Command.BorrowBook;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IBorrowServices
    {
        public Task<ApiResponse<string>> BorrowBook(BorrowBookCommand command);

    }
}
