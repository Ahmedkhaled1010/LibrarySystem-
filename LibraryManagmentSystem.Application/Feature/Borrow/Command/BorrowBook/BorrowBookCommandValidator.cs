using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Borrow.Command.BorrowBook
{
    public class BorrowBookCommandValidator : AbstractValidator<BorrowBookCommand>
    {
        public BorrowBookCommandValidator()
        {
            RuleFor(x => x.BookId).NotEmpty().WithMessage("BookId is required");
        }
    }
}
