using FluentValidation;
using LibraryManagmentSystem.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Application.Feature.Auth.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        private readonly UserManager<User> userManager;

        public ResetPasswordCommandValidator(UserManager<User> userManager)
        {
            RuleFor(x => x.Code)
                .NotNull().MustAsync(IsValidUserAsync).WithMessage("Invalid or expired reset code.");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("New password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.").
                Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.").
                Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.").
                Matches("[0-9]").WithMessage("Password must contain at least one digit.").
                Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
            RuleFor(x => x.ConfirmNewPassword).NotEmpty().WithMessage("New password is required.")
                .Equal(x => x.NewPassword).WithMessage("Password and Confirm Password do not match")
              .MinimumLength(6).WithMessage("Password must be at least 6 characters long.").
              Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.").
              Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.").
              Matches("[0-9]").WithMessage("Password must contain at least one digit.").
              Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
            this.userManager = userManager;
        }
        private async Task<bool> IsValidUserAsync(string code, CancellationToken cancellation)
        {
            if (string.IsNullOrWhiteSpace(code))
                return false;

            var user = await userManager.Users.SingleOrDefaultAsync(u => u.resetPasswordToken == code && u.resetPasswordTokenExpires > DateTime.UtcNow);
            return user != null;
        }
    }
}
