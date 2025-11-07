using LibraryManagmentSystem.Application.IClients;
using LibraryManagmentSystem.Shared.DataTransferModel.Notification;
using LibraryManagmentSystem.Shared.Response;
using System.Net.Http.Json;

namespace LibraryManagmentSystem.Infrastructure.Clients
{
    public class NotificationClient(HttpClient httpClient) : INotificationClient
    {
        public async Task<ApiResponse<IReadOnlyList<NotificationDto>>> GetAllNotificationAdmins()
        {

            var requests = await httpClient.GetFromJsonAsync<IReadOnlyList<NotificationDto>>("https://localhost:7021/api/Notification/admin");
            return new ApiResponse<IReadOnlyList<NotificationDto>>
            {
                Data = requests,
                Success = true,
                Message = "Notification fetched successfully"
            };

        }

        public async Task<ApiResponse<IReadOnlyList<NotificationDto>>> GetAllNotificationByUserId(string id)
        {
            var requests = await httpClient.GetFromJsonAsync<IReadOnlyList<NotificationDto>>($"https://localhost:7021/api/Notification/user?id={id}");
            return new ApiResponse<IReadOnlyList<NotificationDto>>
            {
                Data = requests,
                Success = true,
                Message = "Notification fetched successfully"
            };
        }

        public async Task<ApiResponse<string>> MakeAsRead(Guid id)
        {
            var req = await httpClient.PutAsJsonAsync($"https://localhost:7021/api/Notification?id={id}", "");
            return ApiResponse<string>.Ok("Mark Norification As Read");
        }
    }
}
