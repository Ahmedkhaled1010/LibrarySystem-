using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.UserDto;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(IServicesManager servicesManager) : IRequestHandler<GetUserByIdQuery, ApiResponse<UserDto>>
    {
        public async Task<ApiResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await servicesManager.UserService.GetUserDetailsAsync(request);
            return res;
        }
    }
}
