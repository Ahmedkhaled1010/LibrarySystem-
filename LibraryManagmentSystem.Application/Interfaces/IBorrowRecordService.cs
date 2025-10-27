using LibraryManagmentSystem.Domain.Entity;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface IBorrowRecordService
    {
        Task<BorrowRecord?> GetActiveBorrowAsync(Guid bookId, string userId);
        Task CreateBorrowRecordAsync(Book book, string userId);
    }
}
