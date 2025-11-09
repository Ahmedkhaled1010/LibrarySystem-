using LibraryManagmentSystem.Shared.Model.BasketModule;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Domain.Contracts
{
    public interface IBasketServices
    {
        Task<ApiResponse<CustomerBasket>> GetBasketAsync(string Key);
        Task<ApiResponse<CustomerBasket>> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null);
        Task<ApiResponse<CustomerBasket>> AddItemAsync(BasketItem basketItem, string userId);
        Task<bool> DeleteBasketAsync(string id);
        Task RemoveItemAsync(string userId, string bookId);
        Task UpdateItemQuantityAsync(string userId, string bookId, int newQty);
        Task<decimal> GetTotal(string key);
    }
}
