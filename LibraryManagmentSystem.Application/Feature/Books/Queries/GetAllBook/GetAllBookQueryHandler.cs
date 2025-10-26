using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.Books;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Books.Queries.GetAllBook
{
    public class GetAllBookQueryHandler(IServicesManager servicesManager) : IRequestHandler<GetAllBookQuery, PagedApiResponse<BookDto>>
    {
        public async Task<PagedApiResponse<BookDto>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
        {

            var result = await servicesManager.BookServices.GetAllBooksAsync(request);
            return result;
        }
    }
}
