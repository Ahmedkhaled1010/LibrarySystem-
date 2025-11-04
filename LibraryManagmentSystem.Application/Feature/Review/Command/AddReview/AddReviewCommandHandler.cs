using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Review.Command.AddReview
{
    public class AddReviewCommandHandler(IServicesManager servicesManager) : IRequestHandler<AddReviewCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddReviewCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Validation failed",
                    Errors = errors
                };
            }
            var res = await servicesManager.reviewServices.AddReview(request);
            return res;

        }
    }
}
