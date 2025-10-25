using FluentValidation;

namespace LibraryManagmentSystem.Application.Feature.Auth.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {

            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required");

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.ConfirmEmail)
                .NotEmpty().WithMessage("Confirm Email is required")
                .Equal(x => x.EmailAddress).WithMessage("Email and Confirm Email do not match");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?\d{10,15}$")
                .WithMessage("Invalid phone number format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Password and Confirm Password do not match");

        }
    }
}
