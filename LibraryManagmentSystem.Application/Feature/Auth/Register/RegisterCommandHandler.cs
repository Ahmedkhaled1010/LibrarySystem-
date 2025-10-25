using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Auth.Register
{
    public class RegisterCommandHandler(IAuthServices authServices) : IRequestHandler<RegisterCommand, ApiResponse<RegisterResponse>>
    {
        public async Task<ApiResponse<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validate = new RegisterCommandValidator();
            var validationResult = await validate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
          .Select(e => e.ErrorMessage)
          .ToList();

                return ApiResponse<RegisterResponse>.Fail("Validation failed", errors);
            }

            var result = await authServices.RegisterAsync(request);
            return result;
        }
    }
}
