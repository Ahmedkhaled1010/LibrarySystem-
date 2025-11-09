using LibraryManagmentSystem.Shared.DataTransferModel.Purchases;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Books.Queries.GetAllBookPurchase
{
    public record GetAllBookPurchaseQuery(string userId) : IRequest<ApiResponse<IEnumerable<BookPurchaseDto>>>;

}
