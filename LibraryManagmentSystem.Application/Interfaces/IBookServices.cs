using LibraryManagmentSystem.Application.Feature.Books.Command.CreateBook;
using LibraryManagmentSystem.Application.Feature.Books.Command.DeleteBook;
using LibraryManagmentSystem.Application.Feature.Books.Command.UpdateBook;
using LibraryManagmentSystem.Application.Feature.Books.Queries.GetAllBook;
using LibraryManagmentSystem.Application.Feature.Books.Queries.GetBookById;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.Books;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IBookServices
    {
        Task<ApiResponse<BookDto>> CreateBookAsync(CreateBookCommand createBookCommand);
        Task<PagedApiResponse<BookDto>> GetAllBooksAsync(GetAllBookQuery bookQuery);
        Task<ApiResponse<BookDto>> GetBookAsync(GetBookByIdQuery createBookCommand);
        Task<ApiResponse<BookDto>> UpdateBookAsync(UpdateBookCommand bookCommand);
        Task<ApiResponse<string>> DeleteBookAsync(DeleteBookCommand bookId);
        Task<Book> GetBookAsync(Guid bookId);
        //bool IsAvailable(Book book);
        //void UpdateAvailabilityAsync(Book book, int change);
        void UpdateAvailabilityAsync(Book book, bool avail);

        void UpdateTotalBorrow(Book book);
        void UpdateAvailabilityForSaleAsync(Book book, bool avail);



    }
}
