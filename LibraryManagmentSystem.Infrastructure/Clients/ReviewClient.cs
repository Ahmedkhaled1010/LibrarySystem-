using LibraryManagmentSystem.Application.Feature.Review.Command.AddReview;
using LibraryManagmentSystem.Application.Feature.Review.Query;
using LibraryManagmentSystem.Application.IClients;
using LibraryManagmentSystem.Shared.DataTransferModel.Review;
using LibraryManagmentSystem.Shared.Response;
using MassTransit;
using SharedEventsServices.Events;
using System.Net.Http.Json;

namespace LibraryManagmentSystem.Infrastructure.Clients
{
    public class ReviewClient(HttpClient httpClient, IBus bus) : IReviewClient
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
        public async Task<ApiResponse<IReadOnlyList<ReviewDto>>> GetBookReview(GetBookReviewQuery book)
        {
            var reviews = await httpClient.GetFromJsonAsync<IReadOnlyList<ReviewDto>>($"https://localhost:7178/api/Review?id={book.bookId}");
            return new ApiResponse<IReadOnlyList<ReviewDto>>
            {
                Data = reviews,
                Success = true,
                Message = "Reviews fetched successfully"
            };
        }
    }
}
