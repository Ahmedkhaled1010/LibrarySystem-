using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Review.Query
{
    public class GetBookReviewQueryValidator : AbstractValidator<GetBookReviewQuery>
    {
        public GetBookReviewQueryValidator()
        {
            RuleFor(p => p.bookId).NotEmpty().WithMessage("Book Id Is Required");
        }
    }
}
