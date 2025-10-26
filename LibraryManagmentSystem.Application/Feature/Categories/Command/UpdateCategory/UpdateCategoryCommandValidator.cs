using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Categories.Command.UpdateCategory
{
    class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Category Id is required.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("Invalid Category Id format.");
        }
    }
}
