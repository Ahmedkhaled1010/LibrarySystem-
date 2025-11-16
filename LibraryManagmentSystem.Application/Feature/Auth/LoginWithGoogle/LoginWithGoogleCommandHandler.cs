using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Auth.LoginWithGoogle
{
    public class LoginWithGoogleCommandHandler(IServicesManager servicesManager) : IRequestHandler<LoginWithGoogleCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(LoginWithGoogleCommand request, CancellationToken cancellationToken)
        {
            var res = await servicesManager.AuthServices.GoogleSignInAsync(request);
            return res;
        }
    }
}
