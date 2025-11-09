using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.Purchases;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Books.Queries.GetAllBookPurchase
{
    public class GetAllBookPurchaseQueryHandler(IServicesManager servicesManager) : IRequestHandler<GetAllBookPurchaseQuery, ApiResponse<IEnumerable<BookPurchaseDto>>>
    {
        public async Task<ApiResponse<IEnumerable<BookPurchaseDto>>> Handle(GetAllBookPurchaseQuery request, CancellationToken cancellationToken)
        {
            var res = await servicesManager.bookPurchaseServices.GetAllBookPurchaseByUserIdAsync(request);

            return ApiResponse<IEnumerable<BookPurchaseDto>>.Ok(res);
        }
    }
}
