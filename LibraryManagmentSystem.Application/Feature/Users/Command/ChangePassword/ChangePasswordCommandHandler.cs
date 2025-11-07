using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Users.Command.ChangePassword
{
    public class ChangePasswordCommandHandler(IServicesManager servicesManager) : IRequestHandler<ChangePasswordCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var validate = new ChangePasswordCommanValidator();
            var validationResult = await validate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
          .Select(e => e.ErrorMessage)
          .ToList();

                return ApiResponse<string>.Fail("Validation failed", errors);
            }
            var res = await servicesManager.UserService.ChangePasswordAsync(request);
            return res;
        }
    }
}
