using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Review.Command.UpdateReview
{
    public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidator()
        {
            RuleFor(r => r.Id).NotEmpty().WithMessage("Review Id Is Required");
            RuleFor(p => p.Rating).NotEmpty().WithMessage("Rating Is Required").
             InclusiveBetween(1, 5).WithMessage($"Rating must be between 1 and 5");

        }
    }
}
