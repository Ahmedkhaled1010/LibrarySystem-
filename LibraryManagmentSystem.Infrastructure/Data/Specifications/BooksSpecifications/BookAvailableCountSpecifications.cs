using LibraryManagmentSystem.Domain.Entity;

namespace LibraryManagmentSystem.Infrastructure.Data.Specifications.BooksSpecifications
{
    class BookAvailableCountSpecifications : BaseSpecification<Book, Guid>
    {
        public BookAvailableCountSpecifications(bool isActive) : base(b => b.IsAvailable == isActive)
        {

        }
    }
}
