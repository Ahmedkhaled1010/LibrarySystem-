using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Books.Command.UpdateBook
{
    class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Category Id is required.");

            RuleFor(x => x.CopiesAvailable)
                .GreaterThan(0).WithMessage("Copies available must be greater than zero.");

            RuleFor(x => x.PublishedYear)
                .InclusiveBetween(1400, DateTime.Now.Year).WithMessage($"Published year must be between 1400 and {DateTime.Now.Year}.");
            RuleFor(x => x.BorrowDurationDays).GreaterThan(0).WithMessage("BorrowDurationDays must be greater than zero.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(x => x.CopiesForSaleAvailable)
                .GreaterThanOrEqualTo(0).WithMessage("Copies for sale available cannot be negative.");
            RuleFor(x => x.Pages)
                .GreaterThan(0).WithMessage("Pages must be greater than zero.");


        }
    }
}
