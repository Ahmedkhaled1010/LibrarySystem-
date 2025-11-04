using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Review.AddReview
{
    public class AddReviewQueryValidator : AbstractValidator<AddReviewQuery>
    {
        public AddReviewQueryValidator()
        {
            RuleFor(r => r.BookId).NotEmpty().WithMessage("");
        }
    }
}
