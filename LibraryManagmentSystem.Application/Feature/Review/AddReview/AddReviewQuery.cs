using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Review.AddReview
{
    public class AddReviewQuery : IRequest<ApiResponse<string>>
    {
        public string? Comment { get; set; }
        public string? BookTitle { get; set; }
        public string? UserName { get; set; }
        public Guid? BookId { get; set; }
        public DateTime? Created { get; set; }
        public int? Rating { get; set; }
    }
}
