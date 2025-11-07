using LibraryManagmentSystem.Shared.DataTransferModel.Notification;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.IClients
{
    public interface INotificationClient
    {
        Task<ApiResponse<IReadOnlyList<NotificationDto>>> GetAllNotificationAdmins();
        Task<ApiResponse<IReadOnlyList<NotificationDto>>> GetAllNotificationByUserId(string id);
        Task<ApiResponse<string>> MakeAsRead(Guid id);
    }
}
