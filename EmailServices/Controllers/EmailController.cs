using EmailService.Application.Interfaces;
using EmailService.Domain.Shared.DataTransferModel;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController(IEmailServices emailServices) : ControllerBase
    {
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDto email)
        {
            if (string.IsNullOrEmpty(email.To))
                return BadRequest("Recipient email is required.");

            try
            {
                await emailServices.SendEmailAsync(email);
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error sending email: {ex.Message}");
            }
        }
    }
}
