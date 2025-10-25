using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Model;
using System.Net.Http.Json;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class EmailClient(HttpClient httpClient) : IEmailClient
    {
        public async Task SendEmailAsync(Email email)
        {
            var response = await httpClient.PostAsJsonAsync("api/Email/send", email);
            response.EnsureSuccessStatusCode();
        }
    }
}
