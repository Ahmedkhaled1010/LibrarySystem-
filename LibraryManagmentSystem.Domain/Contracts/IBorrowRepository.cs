using LibraryManagmentSystem.Domain.Entity;

namespace LibraryManagmentSystem.Domain.Contracts
{
    public interface IBorrowRepository : IGenericRepository<BorrowRecord, Guid>
    {
        Task<BorrowRecord?> GetBorrowByMemberAsync(string memberId, Guid bookId);
        Task<List<BorrowRecord>> GetBorrowRecordsByMemberAsync(string memberId);
        Task<List<BorrowRecord>> GetAllBorrowRecordsAsync(ISpecifications<BorrowRecord, Guid> specifications);
        Task<int> GetTotalMemberBorrowed();
        Task<int> GetTotalBookBorrowed();
    }
}
