using LibraryManagmentSystem.Application.Feature.Review.Command.AddReview;
using LibraryManagmentSystem.Application.Feature.Review.Query;
using LibraryManagmentSystem.Shared.DataTransferModel.Review;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.IClients
{
    public interface IReviewClient
    {
        Task<ApiResponse<string>> AddReview(AddReviewCommand addReview);

        Task<ApiResponse<IReadOnlyList<ReviewDto>>> GetBookReview(GetBookReviewQuery query);
    }
}
