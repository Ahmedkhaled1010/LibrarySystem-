using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.Auth;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Auth.Login
{
    public class LoginCommandHandler(IAuthServices authServices) : IRequestHandler<LoginCommand, ApiResponse<AuthDto>>
    {
        public async Task<ApiResponse<AuthDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validator = new LoginCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {

                var errors = validationResult.Errors
          .Select(e => e.ErrorMessage)
          .ToList();

                return ApiResponse<AuthDto>.Fail("Validation failed", errors);

            }
            var result = await authServices.LoginAsync(request);
            return result;
        }
    }
}
