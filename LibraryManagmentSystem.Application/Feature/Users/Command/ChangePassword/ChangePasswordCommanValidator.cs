using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Users.Command.ChangePassword
{
    public class ChangePasswordCommanValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommanValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("CurrentPassword Is Required");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("NewPassword is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("NewPassword and Confirm Password do not match");
        }

    }
}
