using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.AdminDto;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class AdminServices(IServicesManager servicesManager) : IAdminServices
    {
        public async Task<ApiResponse<libraryStats>> GetStats()
        {
            libraryStats libraryStats = new libraryStats();
            libraryStats.TotalUser = await servicesManager.UserService.GetTotalUserAsync();
            libraryStats.TotalBooks = await servicesManager.BookServices.GetTotalBookAsync();
            libraryStats.AvailableBooks = await servicesManager.BookServices.GetTotalAvailable(true);
            libraryStats.UnAvailableBooks = await servicesManager.BookServices.GetTotalAvailable(false);
            libraryStats.TotalBorrowed = await servicesManager.BorrowServices.GetTotalBookBorrowed();
            libraryStats.TotalNotReturned = await servicesManager.BorrowServices.GetTotalBookNotReturn();
            libraryStats.TotalFines = await servicesManager.FineClient.GetTotalFine(true);
            libraryStats.PendingFines = await servicesManager.FineClient.GetTotalFine(false);
            libraryStats.TotalPayments = await servicesManager.paymentServices.GetTotalPayment();

            return ApiResponse<libraryStats>.Ok(libraryStats);

        }
    }
}
