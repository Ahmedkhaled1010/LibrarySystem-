using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.Payment;
using LibraryManagmentSystem.Shared.Response;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using SharedEventsServices.Events;
using System.Net.Http.Json;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class PaymentServices(IServicesManager servicesManager,
        UserManager<User> userManager,
        HttpClient httpClient,
        IBus bus) : IPaymentServices
    {
        public async Task BuyBook(string userId)
        {
            var basket = (await servicesManager.basketServices.GetBasketAsync(userId)).Data;
            var notify = new SucessPaymentEvent
            {
                UserId = userId
            };
            await bus.Publish(notify);
            await servicesManager.bookPurchaseServices.GetFromBasket(basket);
            await servicesManager.basketServices.DeleteBasketAsync(userId);
        }

        public async Task CheckOut(string userId)
        {
            var amount = await servicesManager.basketServices.GetTotal(userId);
            var user = await userManager.FindByIdAsync(userId);
            var CheckoutEvent = new BasketCheckoutEvent
            {
                Amount = amount,
                BasketId = userId,
                Email = user.Email,
                SuccessUrl = $"http://localhost:4200/payment-success/{userId}",
                UserId = userId,

            };
            await bus.Publish(CheckoutEvent);
        }

        public async Task<ApiResponse<IEnumerable<PaymentDto>>> GetPaymentList()
        {
            var res = await httpClient.GetFromJsonAsync<IEnumerable<PaymentDto>>($"https://localhost:7207/api/Fine/admin");
            return ApiResponse<IEnumerable<PaymentDto>>.Ok(res);
        }

        public async Task<decimal> GetTotalPayment()
        {
            var res = await httpClient.GetFromJsonAsync<decimal>($"https://localhost:7207/api/Fine/total-payment");
            return res;
        }

        public async Task PayFine(string userId, string fineId)
        {
            var user = await userManager.FindByIdAsync(userId);

            var fine = new PayFineEvent
            {
                UserId = userId,
                Email = user.Email,
                FineId = fineId
            };
            await bus.Publish(fine);
        }
    }
}
