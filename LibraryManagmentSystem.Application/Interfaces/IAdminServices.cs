using LibraryManagmentSystem.Shared.DataTransferModel.AdminDto;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IAdminServices
    {
        Task<ApiResponse<libraryStats>> GetStats();
    }
}
