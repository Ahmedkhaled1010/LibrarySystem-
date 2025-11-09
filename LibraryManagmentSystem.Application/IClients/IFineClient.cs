using LibraryManagmentSystem.Shared.DataTransferModel.Fine;
using LibraryManagmentSystem.Shared.Model;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.IClients
{
    public interface IFineClient
    {
        Task AddFineAsync(Fine fine);
        Task<ApiResponse<IEnumerable<FineDto>>> GetAllFineByUser(string userId);
    }
}
