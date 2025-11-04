using LibraryManagmentSystem.Application.Feature.Review.Command.AddReview;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IReviewServices
    {
        Task<ApiResponse<string>> AddReview(AddReviewCommand addReview);
    }
}
