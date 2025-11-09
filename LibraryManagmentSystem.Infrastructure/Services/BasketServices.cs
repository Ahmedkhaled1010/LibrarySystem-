using AutoMapper;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Shared.DataTransferModel.BasketDto;
using LibraryManagmentSystem.Shared.DataTransferModel.Books;
using LibraryManagmentSystem.Shared.Model.BasketModule;
using LibraryManagmentSystem.Shared.Response;
using System.Text.Json;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class BasketServices(IServicesManager servicesManager, IMapper mapper) : IBasketServices
    {
        public async Task<ApiResponse<CustomerBasket>> AddItemAsync(BasketItem basketItem, string userId)
        {
            var UserBasket = (await GetBasketAsync(userId)).Data;
            var book = await servicesManager.BookServices.GetBookAsync(basketItem.BookId);
            var bookDto = mapper.Map<BookDto>(book);
            var item = mapper.Map<BasketItemDto>(basketItem);
            item.book = bookDto;
            if (UserBasket is null)
            {
                CustomerBasket customer = new CustomerBasket
                {
                    UserId = userId,

                };
                customer.Items.Add(item);
                return await CreateOrUpdateBasketAsync(customer);
            }
            else
            {
                UserBasket.Items.Add(item);
                return await CreateOrUpdateBasketAsync(UserBasket);


            }
        }

        public async Task<ApiResponse<CustomerBasket>> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var IsCreatedOrUpdated = await servicesManager.casheServices.SetAsync(basket.UserId, basket, TimeToLive ?? TimeSpan.FromDays(30));
            if (IsCreatedOrUpdated)
            {
                return await GetBasketAsync(basket.UserId);
            }
            else
            {
                return ApiResponse<CustomerBasket>.Fail("Can Not Update Or Create Basket Now ,Try Again Later");
            }
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await servicesManager.casheServices.DeleteAsync(id);
        }

        public async Task<ApiResponse<CustomerBasket>> GetBasketAsync(string Key)
        {

            var Basket = await servicesManager.casheServices.GetAsync(Key);
            if (string.IsNullOrEmpty(Basket))
            {
                return ApiResponse<CustomerBasket>.Fail("Basket Not Found");
            }
            else
            {
                var basket = JsonSerializer.Deserialize<CustomerBasket>(Basket!); ;
                return ApiResponse<CustomerBasket>.Ok(basket, "Basket Fetch Successfuly");
            }

        }

        public async Task<decimal> GetTotal(string key)
        {
            var basket = (await GetBasketAsync(key)).Data;
            decimal total = 0;
            foreach (var item in basket.Items)
            {

                total += item.book.Price;
            }
            return total;
        }

        public async Task RemoveItemAsync(string userId, string bookId)
        {
            var res = await GetBasketAsync(userId);
            var basket = res.Data;
            if (basket == null) return;
            var item = basket.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item != null)
            {
                basket.Items.Remove(item);
                await CreateOrUpdateBasketAsync(basket);
            }



        }

        public async Task UpdateItemQuantityAsync(string userId, string bookId, int newQty)
        {
            var res = await GetBasketAsync(userId);
            var basket = res.Data;
            if (basket == null) return;
            var item = basket.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item != null)
            {
                item.Quantity = newQty;
                await CreateOrUpdateBasketAsync(basket);
            }
        }
    }
}
