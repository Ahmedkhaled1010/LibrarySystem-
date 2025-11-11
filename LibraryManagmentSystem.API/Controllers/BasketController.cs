using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Model.BasketModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController(IServicesManager servicesManager) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetBasket()

        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var Basket = await servicesManager.basketServices.GetBasketAsync(user);
            return Ok(Basket);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasketAsync(BasketItem basket)

        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Basket = await servicesManager.basketServices.AddItemAsync(basket, user);
            return Ok(Basket);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasket([FromQuery] string key)
        {
            var Res = await servicesManager.basketServices.DeleteBasketAsync(key);
            return Ok(Res);
        }
        [HttpDelete("{item}")]
        public async Task<IActionResult> DeleteItemBasket(string item)

        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await servicesManager.basketServices.RemoveItemAsync(user, item);
            return Ok();
        }
        [HttpPut("{key}")]
        public async Task<IActionResult> UpdateItemBasket(string item, int quantity)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await servicesManager.basketServices.UpdateItemQuantityAsync(user, item, quantity);
            return Ok();
        }

    }
}
