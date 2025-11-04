using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Review.Query.GetBookAvgRate
{
    public record GetBookAvgRateQuery(Guid bookId) : IRequest<ApiResponse<double>>;

}
