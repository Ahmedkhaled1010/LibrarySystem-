using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Users.Command.UploadProfileImage
{
    public class UploadProfileImageCommandHandler(IServicesManager servicesManager) : IRequestHandler<UploadProfileImageCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(UploadProfileImageCommand request, CancellationToken cancellationToken)
        {
            var res = await servicesManager.UserService.UploadProfileIamge(request);
            return res;
        }
    }
}
