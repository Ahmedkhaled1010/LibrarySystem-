using LibraryManagmentSystem.Domain.Entity;

namespace LibraryManagmentSystem.Infrastructure.Data.Specifications.BookPurchaseSpecifications
{
    class BookPurchaseSpecification : BaseSpecification<BookPurchase, Guid>
    {
        public BookPurchaseSpecification(string userId) : base(p => (p.UserId == userId))
        {
            AddInclude(b => b.Book);
            AddInclude(b => b.Book.Author);
        }
    }
}
