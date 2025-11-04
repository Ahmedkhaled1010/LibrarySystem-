using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Books.Command.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.CopiesAvailable)
                .GreaterThan(0).WithMessage("Copies available must be greater than zero.");
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Category is required.");
            RuleFor(x => x.PublishedYear)
                .InclusiveBetween(1400, DateTime.Now.Year).WithMessage($"Published year must be between 1400 and {DateTime.Now.Year}.");
            RuleFor(x => x.BorrowDurationDays).GreaterThan(0).WithMessage("BorrowDurationDays must be greater than zero.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Language).NotEmpty().WithMessage("Language is required.");
            RuleFor(x => x.CopiesForSaleAvailable)
                .GreaterThanOrEqualTo(0).WithMessage("Copies for sale available cannot be negative.");
            RuleFor(x => x.Pages)
                .GreaterThan(0).WithMessage("Pages must be greater than zero.");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.");


        }
    }
}
