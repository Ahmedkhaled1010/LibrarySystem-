using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Books.Command.DeleteBook
{
    class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Category Id must not be empty.");
        }
    }
}
