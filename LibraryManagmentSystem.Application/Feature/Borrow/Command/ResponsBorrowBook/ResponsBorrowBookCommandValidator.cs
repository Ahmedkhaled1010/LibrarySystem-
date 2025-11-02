using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Borrow.Command.ResponsBorrowBook
{
    public class ResponsBorrowBookCommandValidator : AbstractValidator<ResponsBorrowBookCommand>
    {
        public ResponsBorrowBookCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.BookId).NotEmpty().WithMessage("BookId is required");
            RuleFor(x => x.RequestId).NotEmpty().WithMessage("RequestId is required");
            RuleFor(x => x.IsApproved).NotNull().WithMessage("IsApproved is required");

        }
    }
}
