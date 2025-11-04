using LibraryManagmentSystem.Application.Feature.Review.Command.AddReview;
using LibraryManagmentSystem.Application.Feature.Review.Query.GetBookAvgRate;
using LibraryManagmentSystem.Application.Feature.Review.Query.GetBookReview;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Addreview(AddReviewCommand query)
        {
            var res = await mediator.Send(query);
            return Ok(res);

        }
        [HttpGet]
        public async Task<IActionResult> GetReviews([FromQuery] GetBookReviewQuery query)
        {
            var res = await mediator.Send(query);
            return Ok(res);
        }
        [HttpGet("avgRating")]
        public async Task<IActionResult> AverageBookRate([FromQuery] GetBookAvgRateQuery query)
        {
            var res = await mediator.Send(query);
            return Ok(res);
        }


    }
}
