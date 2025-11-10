using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.RequestDto;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Borrow.Queries.GetAllRequest
{
    public class GetAllRequestQueryHandler(IServicesManager services) : IRequestHandler<GetAllRequestQuery, ApiResponse<IReadOnlyList<RequestDto>>>
    {
        public async Task<ApiResponse<IReadOnlyList<RequestDto>>> Handle(GetAllRequestQuery request, CancellationToken cancellationToken)
        {
            return await services.requestClient.GetAllRequest(request);
        }
    }
}
