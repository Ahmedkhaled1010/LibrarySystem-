using LibraryManagmentSystem.Shared.DataTransferModel.Review;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Review.Query.GetAllBookAvgRate
{
    public record GetAllBookAvgRateQuery : IRequest<ApiResponse<IReadOnlyList<BookRatingAvgDto>>>;

}
