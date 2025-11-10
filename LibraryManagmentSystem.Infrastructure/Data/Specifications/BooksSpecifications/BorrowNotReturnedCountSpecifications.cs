using LibraryManagmentSystem.Domain.Entity;

namespace LibraryManagmentSystem.Infrastructure.Data.Specifications.BooksSpecifications
{
    class BorrowNotReturnedCountSpecifications : BaseSpecification<BorrowRecord, Guid>
    {
        public BorrowNotReturnedCountSpecifications() : base(b => b.ReturnDate != null)
        {

        }
    }
}
