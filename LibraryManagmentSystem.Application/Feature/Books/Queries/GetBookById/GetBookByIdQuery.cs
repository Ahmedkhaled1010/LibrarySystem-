using LibraryManagmentSystem.Shared.DataTransferModel.Books;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Books.Queries.GetBookById
{
    public record GetBookByIdQuery(string Id) : IRequest<ApiResponse<BookDto>>;

}
