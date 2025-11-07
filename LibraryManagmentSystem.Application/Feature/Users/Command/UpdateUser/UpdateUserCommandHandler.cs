using LibraryManagmentSystem.Application.Feature.Users.Command.NewFolder;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.UserDto;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Users.Command.UpdateUser
{
    public class UpdateUserCommandHandler(IServicesManager servicesManager) : IRequestHandler<UpdateUserCommand, ApiResponse<UserDto>>
    {
        public async Task<ApiResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var res = await servicesManager.UserService.UpdateUserDetailsAsync(request);
            return res;
        }
    }
}
