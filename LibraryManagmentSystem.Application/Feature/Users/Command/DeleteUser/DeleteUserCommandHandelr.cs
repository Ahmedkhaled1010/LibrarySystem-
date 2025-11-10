using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Users.Command.DeleteUser
{
    public class DeleteUserCommandHandelr(IServicesManager servicesManager) : IRequestHandler<DeleteUserCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var res = await servicesManager.UserService.DeleteUserAsync(request);
            return res;
        }
    }
}
