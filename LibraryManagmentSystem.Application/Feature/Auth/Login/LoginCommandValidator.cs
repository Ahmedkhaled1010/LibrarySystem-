using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Auth.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(RuleFor => RuleFor.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email format");
            RuleFor(RuleFor => RuleFor.Password).NotEmpty().WithMessage("Password is required").MinimumLength(6).WithMessage("Password must be at least 6 characters long");
        }
    }
}
