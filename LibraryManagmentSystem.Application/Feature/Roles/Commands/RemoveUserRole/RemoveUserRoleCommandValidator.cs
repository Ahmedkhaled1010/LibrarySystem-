using FluentValidation;
using LibraryManagmentSystem.Application.Features.Roles.Commands.RemoveUserRole;
using LibraryManagmentSystem.Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.ApplicationFeatures.Roles.Commands.RemoveUserRole
{
    public class RemoveUserRoleCommandValidator : AbstractValidator<RemoveUserRoleCommand>
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public RemoveUserRoleCommandValidator(UserManager<User> userManager,
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
