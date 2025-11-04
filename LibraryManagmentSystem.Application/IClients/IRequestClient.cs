using LibraryManagmentSystem.Shared.DataTransferModel.RequestDto;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.IClients
{
    public interface IRequestClient
    {
        Task<ApiResponse<IReadOnlyList<RequestDto>>> GetAllRequest();
    }
}
