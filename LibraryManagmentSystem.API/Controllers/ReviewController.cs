using LibraryManagmentSystem.Application.Feature.Review.Command.AddReview;
using LibraryManagmentSystem.Application.Feature.Review.Command.DeleteReview;
using LibraryManagmentSystem.Application.Feature.Review.Command.UpdateReview;
using LibraryManagmentSystem.Application.Feature.Review.Query.GetAllBookAvgRate;
using LibraryManagmentSystem.Application.Feature.Review.Query.GetBookAvgRate;
using LibraryManagmentSystem.Application.Feature.Review.Query.GetBookReview;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AverageBookRate([FromQuery] GetBookAvgRateQuery query)
        {
            var res = await mediator.Send(query);
            return Ok(res);
        }
        [HttpGet("avgRatingAll")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AverageAllBookRate()
        {
            var res = await mediator.Send(new GetAllBookAvgRateQuery());
            return Ok(res);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete([FromQuery] DeleteReviewCommand command)
        {

            var res = await mediator.Send(command);
            return Ok(res);
        }
        [HttpPut]

        public async Task<IActionResult> Update(UpdateReviewCommand command)
        {

            var res = await mediator.Send(command);
            return Ok(res);
        }



    }
}
