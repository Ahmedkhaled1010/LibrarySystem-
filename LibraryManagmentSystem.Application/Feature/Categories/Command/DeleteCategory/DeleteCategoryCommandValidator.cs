using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Categories.Command.DeleteCategory
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>

    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Category Id must not be empty.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("Category Id must be a valid GUID.");
        }
    }
}
