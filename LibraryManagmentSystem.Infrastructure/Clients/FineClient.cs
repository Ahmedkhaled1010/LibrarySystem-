using LibraryManagmentSystem.Application.IClients;
using LibraryManagmentSystem.Shared.Model;
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
    }
}
