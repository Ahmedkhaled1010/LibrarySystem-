using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.UserDto;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Users.Queries.GetAllUser
{
    public class GetAllUserQueryHandler(IServicesManager servicesManager) : IRequestHandler<GetAllUserQuery, ApiResponse<IEnumerable<UserDto>>>
    {
        public async Task<ApiResponse<IEnumerable<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var res = await servicesManager.UserService.GetAllUser();
            return res;
        }
    }
}
