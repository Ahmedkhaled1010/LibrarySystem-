using FluentValidation;

namespace LibraryManagmentSystem.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommandValidation : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidation()
        {
            RuleFor(p => p.RoleName).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
