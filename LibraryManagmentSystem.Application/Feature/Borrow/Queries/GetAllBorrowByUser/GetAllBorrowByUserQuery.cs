using LibraryManagmentSystem.Shared.DataTransferModel.Borrow;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Borrow.Queries.GetAllBorrowByUser
{
    public class GetAllBorrowByUserQuery : IRequest<ApiResponse<IEnumerable<BorrowRecordDto>>>
    {
        public string UserId { get; set; } = default!;
    }
}
