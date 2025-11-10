using LibraryManagmentSystem.Application.Feature.Borrow.Queries.GetAllRequest;
using LibraryManagmentSystem.Shared.DataTransferModel.RequestDto;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.IClients
{
    public interface IRequestClient
    {
        Task<ApiResponse<IReadOnlyList<RequestDto>>> GetAllRequest(GetAllRequestQuery query);
    }
}
