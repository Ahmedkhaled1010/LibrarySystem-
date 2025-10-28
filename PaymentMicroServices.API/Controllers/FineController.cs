using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentMicroServices.Application.Features.Fines.Command;

namespace PaymentMicroServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FineController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateFine(AddFineCommand createFine)
        {
            var result = await mediator.Send(createFine);
            return Ok(result);
        }
    }
}
