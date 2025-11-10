using LibraryManagmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController(IServicesManager servicesManager) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromQuery] string bookId)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await servicesManager.favoriteCacheService.AddToFavoriteAsync(user, bookId);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery] string bookId)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await servicesManager.favoriteCacheService.RemoveFromFavoriteAsync(user, bookId);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(await servicesManager.favoriteCacheService.GetFavoritesAsync(user));
        }
    }
}
