namespace LibraryManagmentSystem.Shared.DataTransferModel.Notification
{
    public class NotificationDto
    {
        public Guid? Id { get; set; }
        public bool? IsDeleted { get; set; }
        public string UserId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Message { get; set; } = default!;
        public bool IsRead { get; set; } = false;
        public DateTime Timestamp { get; set; }
        public string Type { get; set; } = default!;
        public string Target { get; set; } = default!;

    }
}
