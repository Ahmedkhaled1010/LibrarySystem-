using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Borrow.Command.ReturnBook
{
    public class ReturnBookCommandValidator : AbstractValidator<ReturnBookCommand>
    {
        public ReturnBookCommandValidator()
        {
            RuleFor(x => x.BookId).NotEmpty().WithMessage("BookId is required");

        }
    }
}
