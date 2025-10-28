using FluentValidation;

namespace PaymentMicroServices.Application.Features.Fines.Command
{
    public class AddFineCommandValidator : AbstractValidator<AddFineCommand>
    {
        public AddFineCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
            RuleFor(x => x.Reason).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Reason is required.")
                .MaximumLength(250).WithMessage("Reason cannot exceed 250 characters.");
            RuleFor(x => x.DateIssued).NotNull()
                .LessThanOrEqualTo(DateTime.Now).WithMessage("DateIssued cannot be in the future.");
        }
    }
}
