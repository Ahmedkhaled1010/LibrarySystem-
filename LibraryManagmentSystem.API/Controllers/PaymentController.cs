using LibraryManagmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IServicesManager servicesManager) : ControllerBase
    {
        [HttpGet("buy-book")]
        public async Task<IActionResult> BuyBook(string userId)
        {
            await servicesManager.paymentServices.BuyBook(userId);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await servicesManager.paymentServices.CheckOut(user);
            return Ok();
        }

    }
}
