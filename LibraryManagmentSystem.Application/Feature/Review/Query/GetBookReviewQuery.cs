using LibraryManagmentSystem.Shared.DataTransferModel.Review;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Review.Query
{
    public record GetBookReviewQuery(Guid bookId) : IRequest<ApiResponse<IReadOnlyList<ReviewDto>>>;

}
