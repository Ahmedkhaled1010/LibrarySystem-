using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Review.Command.DeleteReview
{
    public record DeleteReviewCommand(Guid id) : IRequest<ApiResponse<string>>;

}
