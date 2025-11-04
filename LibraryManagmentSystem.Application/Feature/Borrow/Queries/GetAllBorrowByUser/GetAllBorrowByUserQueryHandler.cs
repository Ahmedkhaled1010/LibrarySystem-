using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.Borrow;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Borrow.Queries.GetAllBorrowByUser
{
    public class GetAllBorrowByUserQueryHandler(IServicesManager servicesManager) : IRequestHandler<GetAllBorrowByUserQuery, ApiResponse<IEnumerable<BorrowRecordDto>>>
    {
        public async Task<ApiResponse<IEnumerable<BorrowRecordDto>>> Handle(GetAllBorrowByUserQuery request, CancellationToken cancellationToken)
        {
            var borrowRecords = await servicesManager.BorrowServices.BorrowingHistory(request);
            return borrowRecords;
        }

    }
}
