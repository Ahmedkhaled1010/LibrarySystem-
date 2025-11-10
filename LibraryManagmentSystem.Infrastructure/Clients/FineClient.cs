using LibraryManagmentSystem.Application.IClients;
using LibraryManagmentSystem.Shared.DataTransferModel.Fine;
using LibraryManagmentSystem.Shared.Model;
using LibraryManagmentSystem.Shared.Response;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace LibraryManagmentSystem.Infrastructure.Clients
{
    public class FineClient(HttpClient httpClient, ILogger<FineClient> logger) : IFineClient
    {
        public async Task AddFineAsync(Fine fine)
        {
            var response = await httpClient.PostAsJsonAsync("api/Fine", fine);

            var x = response.EnsureSuccessStatusCode();

            logger.LogInformation(x.ToString());


        }

        public async Task<ApiResponse<IEnumerable<FineDto>>> GetAllFineByUser(string userId)
        {
            var res = await httpClient.GetFromJsonAsync<IEnumerable<FineDto>>($"https://localhost:7207/api/Fine?userId={userId}");
            return ApiResponse<IEnumerable<FineDto>>.Ok(res);
        }
        public async Task<ApiResponse<IEnumerable<FineDto>>> GetAllFine()
        {
            var res = await httpClient.GetFromJsonAsync<IEnumerable<FineDto>>($"https://localhost:7207/api/Fine/admin");
            return ApiResponse<IEnumerable<FineDto>>.Ok(res);
        }

        public async Task<decimal> GetTotalFine(bool paid)
        {

            var res = await httpClient.GetFromJsonAsync<decimal>($"https://localhost:7207/api/Fine/total-fine/{paid}");
            return res;
        }
    }
}
