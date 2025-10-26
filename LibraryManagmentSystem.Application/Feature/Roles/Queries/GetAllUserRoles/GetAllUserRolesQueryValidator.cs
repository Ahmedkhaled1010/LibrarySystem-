using FluentValidation;

namespace LibraryManagmentSystem.Application.Features.Roles.Queries.GetAllUserRoles
{
    public class GetAllUserRolesQueryValidator : AbstractValidator<GetAllUserRolesQuery>
    {
        public GetAllUserRolesQueryValidator()
        {
            RuleFor(p => p.UserId).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
