using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Entity;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using SharedEventsServices.Events;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class PaymentServices(IServicesManager servicesManager,
        UserManager<User> userManager,
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
                SuccessUrl = $"https://localhost:7164/api/Payment/buy-book?userId={user.Id}",
                UserId = userId,

            };
            await bus.Publish(CheckoutEvent);

        }
    }
}
