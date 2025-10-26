using LibraryManagmentSystem.Shared.DataTransferModel.Books;
using LibraryManagmentSystem.Shared.QueryParams;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Books.Queries.GetAllBook
{
    public record GetAllBookQuery(BookQueryParams BookQueryParams) : IRequest<PagedApiResponse<BookDto>>;

}
