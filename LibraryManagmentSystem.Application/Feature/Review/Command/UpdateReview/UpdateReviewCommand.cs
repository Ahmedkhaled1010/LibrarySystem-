using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Review.Command.UpdateReview
{
    public class UpdateReviewCommand : IRequest<ApiResponse<string>>
    {
        public Guid Id { get; set; }

        public string? Comment { get; set; }
        public int? Rating { get; set; }
    }
}
