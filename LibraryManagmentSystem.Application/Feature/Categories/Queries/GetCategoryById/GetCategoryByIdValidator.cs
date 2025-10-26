using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Categories.Queries.GetCategoryById
{
    class GetCategoryByIdValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Category Id is required.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("Invalid Category Id format.");
        }
    }
}
