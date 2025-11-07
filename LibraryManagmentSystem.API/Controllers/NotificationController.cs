using LibraryManagmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController(IServicesManager servicesManager) : ControllerBase
    {
        [HttpGet("admin")]
        public async Task<IActionResult> GetAllNotificationAdmin()
        {
            var res = await servicesManager.notificationClient.GetAllNotificationAdmins();
            return Ok(res);
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetAllNotificationUser(string id)
        {
            var res = await servicesManager.notificationClient.GetAllNotificationByUserId(id);
            return Ok(res);
        }
        [HttpPut]
        public async Task<IActionResult> MarkNotificationRead(Guid id)
        {
            await servicesManager.notificationClient.MakeAsRead(id);
            return Ok();
        }

    }
}
