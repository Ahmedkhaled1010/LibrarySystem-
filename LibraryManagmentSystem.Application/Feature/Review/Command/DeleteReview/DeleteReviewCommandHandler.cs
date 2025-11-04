using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Review.Command.DeleteReview
{
    public class DeleteReviewCommandHandler(IServicesManager servicesManager) : IRequestHandler<DeleteReviewCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteReviewCommandValidator();
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
            var res = await servicesManager.reviewServices.DeleteReview(request);
            return res;
        }
    }
}
