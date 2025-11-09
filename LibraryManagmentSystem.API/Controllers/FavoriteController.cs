using LibraryManagmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController(IServicesManager servicesManager) : ControllerBase
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromQuery] string userId, [FromQuery] string bookId)
        {
            await servicesManager.favoriteCacheService.AddToFavoriteAsync(userId, bookId);
            return Ok("Added");
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> Remove([FromQuery] string userId, [FromQuery] string bookId)
        {
            await servicesManager.favoriteCacheService.RemoveFromFavoriteAsync(userId, bookId);
            return Ok("Removed");
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get([FromQuery] string userId)
        {
            return Ok(await servicesManager.favoriteCacheService.GetFavoritesAsync(userId));
        }
    }
}
