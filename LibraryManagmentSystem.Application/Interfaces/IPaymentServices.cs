using LibraryManagmentSystem.Shared.DataTransferModel.Payment;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IPaymentServices
    {
        Task BuyBook(string userId);
        Task CheckOut(string userId);
        Task PayFine(string userId, string fineId);
        Task<ApiResponse<IEnumerable<PaymentDto>>> GetPaymentList();
        Task<decimal> GetTotalPayment();
        Task SuccessPayFine(string fineId);
    }
}
