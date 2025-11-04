using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.Review;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Review.Query.GetAllBookAvgRate
{
    public class GetAllBookAvgRateQueryHandler(IServicesManager servicesManager) :
        IRequestHandler<GetAllBookAvgRateQuery, ApiResponse<IReadOnlyList<BookRatingAvgDto>>>
    {
        public async Task<ApiResponse<IReadOnlyList<BookRatingAvgDto>>> Handle(GetAllBookAvgRateQuery request, CancellationToken cancellationToken)
        {
            var res = await servicesManager.reviewServices.AverageAllBookRate();
            return res;
        }
    }
}
