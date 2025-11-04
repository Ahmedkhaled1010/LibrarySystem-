using LibraryManagmentSystem.Application.IClients;
using LibraryManagmentSystem.Shared.DataTransferModel.RequestDto;
using LibraryManagmentSystem.Shared.Response;
using System.Net.Http.Json;

namespace LibraryManagmentSystem.Infrastructure.Clients
{
    public class RequestClient(HttpClient httpClient) : IRequestClient
    {
        public async Task<ApiResponse<IReadOnlyList<RequestDto>>> GetAllRequest()
        {
            var requests = await httpClient.GetFromJsonAsync<IReadOnlyList<RequestDto>>("https://localhost:7021/api/Request");
            return new ApiResponse<IReadOnlyList<RequestDto>>
            {
                Data = requests,
                Success = true,
                Message = "Requests fetched successfully"
            };

        }

    }
}
