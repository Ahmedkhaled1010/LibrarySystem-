using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.Review;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Review.Query.GetBookReview
{
    public class GetBookReviewQueryHandler(IServicesManager servicesManager) : IRequestHandler<GetBookReviewQuery, ApiResponse<IReadOnlyList<ReviewDto>>>
    {
        public async Task<ApiResponse<IReadOnlyList<ReviewDto>>> Handle(GetBookReviewQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetBookReviewQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ApiResponse<IReadOnlyList<ReviewDto>>.Fail("Validation Failed", errors);
            }
            var res = await servicesManager.reviewServices.GetBookReview(request);
            return res;
        }
    }
}
