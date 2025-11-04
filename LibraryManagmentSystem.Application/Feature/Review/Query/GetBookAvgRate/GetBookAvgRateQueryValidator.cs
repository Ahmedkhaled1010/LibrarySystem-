using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Review.Query.GetBookAvgRate
{
    public class GetBookAvgRateQueryValidator : AbstractValidator<GetBookAvgRateQuery>
    {
        public GetBookAvgRateQueryValidator()
        {
            RuleFor(r => r.bookId).NotEmpty().WithMessage("Book Id Is Required");
        }
    }
}
