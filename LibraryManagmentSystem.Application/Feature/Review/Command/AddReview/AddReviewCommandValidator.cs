using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Review.Command.AddReview
{
    public class AddReviewCommandValidator : AbstractValidator<AddReviewCommand>
    {
        public AddReviewCommandValidator()
        {
            RuleFor(p => p.BookId).NotEmpty().WithMessage("BookId Is Required");
            RuleFor(p => p.BookTitle).NotEmpty().WithMessage("BookTitle Is Required");
            RuleFor(p => p.UserName).NotEmpty().WithMessage("UserName Is Required");

            RuleFor(p => p.Comment).NotEmpty().WithMessage("Comment Is Required");
            RuleFor(p => p.Rating).NotEmpty().WithMessage("Rating Is Required").
             InclusiveBetween(1, 5).WithMessage($"Rating must be between 1 and 5");

            ;



        }
    }
}
