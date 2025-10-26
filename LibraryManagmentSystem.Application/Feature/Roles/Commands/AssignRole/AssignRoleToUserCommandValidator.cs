using FluentValidation;
using LibraryManagmentSystem.Applicationr.Features.Roles.Commands.AssignRole;
using LibraryManagmentSystem.Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.Application.Commands.AssignRole
{
    public class AssignRoleToUserCommandValidator : AbstractValidator<AssignRoleToUserCommand>
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public AssignRoleToUserCommandValidator(UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            RuleFor(p => p.UserId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(p => p.RoleName).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(p => p.UserId).MustAsync(BeAValidUserId).WithMessage("User with Id {PropertyValue} does not exist.");
            RuleFor(p => p.RoleName).MustAsync(BeAValidRoleName).WithMessage("Role with name {PropertyValue} does not exist.");

        }
        private async Task<bool> BeAValidUserId(string userId, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user != null;
        }
        private async Task<bool> BeAValidRoleName(string roleName, CancellationToken cancellationToken)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            return role != null;
        }

    }
}
