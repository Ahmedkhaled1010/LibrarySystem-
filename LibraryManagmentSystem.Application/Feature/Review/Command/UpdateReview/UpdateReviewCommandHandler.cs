using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Review.Command.UpdateReview
{
    public class UpdateReviewCommandHandler(IServicesManager servicesManager) : IRequestHandler<UpdateReviewCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateReviewCommandValidator();
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
            var res = await servicesManager.reviewServices.UpdateReview(request);
            return res;
        }
    }
}
