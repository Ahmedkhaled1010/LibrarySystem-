using FluentValidation;

namespace LibraryManagmentSystem.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(p => p.RoleName).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
