using LibraryManagmentSystem.Application.Feature.Books.Queries.GetAllBookPurchase;
using LibraryManagmentSystem.Shared.DataTransferModel.Purchases;
using LibraryManagmentSystem.Shared.Model.BasketModule;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IBookPurchaseServices
    {
        Task AddBookPurchaseAsync(string userId, Guid BookId);
        Task GetFromBasket(CustomerBasket basket);
        Task<IEnumerable<BookPurchaseDto>> GetAllBookPurchaseByUserIdAsync(GetAllBookPurchaseQuery userId);
    }
}
