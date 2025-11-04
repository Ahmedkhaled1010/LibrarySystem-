using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Review.Query.GetBookAvgRate
{
    public class GetBookAvgRateQueryHandler(IServicesManager servicesManager) : IRequestHandler<GetBookAvgRateQuery, ApiResponse<double>>
    {
        public async Task<ApiResponse<double>> Handle(GetBookAvgRateQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetBookAvgRateQueryValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ApiResponse<double>.Fail("Validation Failed", errors);
            }
            var res = await servicesManager.reviewServices.AverageBookRate(request);
            return res;
        }
    }
}
