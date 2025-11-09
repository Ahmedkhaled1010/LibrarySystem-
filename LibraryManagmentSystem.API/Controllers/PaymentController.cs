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
        public async Task<IActionResult> BuyBook()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await servicesManager.paymentServices.BuyBook(user);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await servicesManager.paymentServices.CheckOut(user);
            return Ok();
        }
        [HttpGet("pay-fine")]
        public async Task<IActionResult> PayFine(string fineId)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await servicesManager.paymentServices.PayFine(user, fineId);
            return Ok();
        }

    }
}
