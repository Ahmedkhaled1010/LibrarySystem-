using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.Application.Feature.Auth.ResetPassword
{
    public class ResetPasswordCommandHandler(IServicesManager servicesManager,
        UserManager<User> userManager
        ) : IRequestHandler<ResetPasswordCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new ResetPasswordCommandValidator(userManager);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ApiResponse<bool>.Fail("Validation failed", errors);
            }
            var result = await servicesManager.AuthServices.ResetPasswordAsync(request);
            return result;
        }
    }
}
