using LibraryManagmentSystem.Application.Feature.Review.Command.AddReview;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MassTransit;
using SharedEventsServices.Events;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class ReviewServices(IBus bus) : IReviewServices
    {
        public async Task<ApiResponse<string>> AddReview(AddReviewCommand addReview)
        {
            var review = new ReviewBookEvent
            {
                BookId = addReview.BookId,
                UserName = addReview.UserName,
                Comment = addReview.Comment,
                Rating = addReview.Rating,
                Created = DateTime.Now,
                BookTitle = addReview.BookTitle


            };
            await bus.Publish(review);
            return ApiResponse<string>.Ok("Review Add Successfuly", "Reviews");
        }
    }
}
