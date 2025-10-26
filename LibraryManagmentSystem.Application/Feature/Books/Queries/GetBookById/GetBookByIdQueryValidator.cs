using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Books.Queries.GetBookById
{
    class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Book Id cannot be empty");
        }
    }
}
