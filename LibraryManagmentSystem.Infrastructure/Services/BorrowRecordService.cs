using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Domain.Enum.BorrowRecord;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class BorrowRecordService(IUnitOfWork unitOfWork) : IBorrowRecordService
    {
        private readonly IBorrowRepository borrowRepository = unitOfWork.borrowRepository;
        public async Task CreateBorrowRecordAsync(Book book, string userId)
        {
            var borrowRecord = new BorrowRecord
            {
                BookId = book.Id,
                UserId = userId,
                BorrowDate = DateTime.UtcNow,

            };
            borrowRecord.SetActualReturnDate(book);

            await borrowRepository.AddAsync(borrowRecord);
        }

        public async Task<BorrowRecord?> GetActiveBorrowAsync(Guid bookId, string userId)
        {
            var Borrow = await borrowRepository.GetBorrowByMemberAsync(userId, bookId);
            return Borrow;
        }

        public void SetReturnDate(BorrowRecord Borrow)
        {
            Borrow.ReturnDate = DateTime.UtcNow;
            borrowRepository.Update(Borrow);
        }
        public void UpdateStatus(BorrowRecord Borrow)
        {
            Borrow.Status = BorrowRecordStatus.returned.ToString();
            borrowRepository.Update(Borrow);
        }
    }
}
