namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IPaymentServices
    {
        Task BuyBook(string userId);
        Task CheckOut(string userId);
    }
}
