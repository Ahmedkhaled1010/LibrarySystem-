using LibraryManagmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FineController(IServicesManager servicesManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllFines()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var res = await servicesManager.FineClient.GetAllFineByUser(user);
            return Ok(res);

        }
    }
}
