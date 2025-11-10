using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Infrastructure.Data.Context;
using LibraryManagmentSystem.Infrastructure.Data.Specifications;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Infrastructure.Data.Repositories
{
    public class BorrowRepository(LibraryDbContext dbContext) : GenericRepository<BorrowRecord, Guid>(dbContext), IBorrowRepository
    {
        public async Task<List<BorrowRecord>> GetAllBorrowRecordsAsync(ISpecifications<BorrowRecord, Guid> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(dbContext.borrowRecords, specifications).ToListAsync();

        }

        public async Task<BorrowRecord?> GetBorrowByMemberAsync(string memberId, Guid bookId)
        {
            return await dbContext.borrowRecords.FirstOrDefaultAsync(b => b.UserId == memberId
                                 && b.BookId == bookId
                                 && b.ReturnDate == null);
        }

        public async Task<List<BorrowRecord>> GetBorrowRecordsByMemberAsync(string memberId)
        {
            return await dbContext.borrowRecords
               .Include(b => b.Book).
               ThenInclude(b => b.Author).
               Include(b => b.User)
               .Where(b => b.UserId == memberId)
               .ToListAsync();
        }

        public async Task<int> GetTotalBookBorrowed()
        {
            return await dbContext.borrowRecords.
                Include(b => b.Book).
                Select(b => b.Book)


                .CountAsync();
        }

        public async Task<int> GetTotalMemberBorrowed()
        {
            return await dbContext.borrowRecords.
                Include(b => b.User).
                Select(b => b.User).
                Distinct()
                .CountAsync();
        }
    }
}
