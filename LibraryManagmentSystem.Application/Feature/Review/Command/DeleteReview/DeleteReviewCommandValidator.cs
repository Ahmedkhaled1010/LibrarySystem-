using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Review.Command.DeleteReview
{
    public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
    {
        public DeleteReviewCommandValidator()
        {
            RuleFor(r => r.id).NotEmpty().WithMessage("Id is required");
        }
    }
}
