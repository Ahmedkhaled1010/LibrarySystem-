using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Categories.Command.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Category name is required.");

            RuleFor(p => p.Description).NotEmpty().WithMessage("Category Description is required.").MaximumLength(500).WithMessage("Category description must not exceed 500 characters.");
        }
    }
}
