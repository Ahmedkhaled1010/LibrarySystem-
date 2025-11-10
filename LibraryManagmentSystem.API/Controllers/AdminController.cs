using LibraryManagmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IServicesManager servicesManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetState()
        {

            var res = await servicesManager.adminServices.GetStats();
            return Ok(res);
        }
    }
}
