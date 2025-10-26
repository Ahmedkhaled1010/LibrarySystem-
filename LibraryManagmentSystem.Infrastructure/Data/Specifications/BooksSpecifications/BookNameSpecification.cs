using LibraryManagmentSystem.Domain.Entity;

namespace LibraryManagmentSystem.Infrastructure.Data.Specifications.BooksSpecifications
{
    class BookNameSpecification : BaseSpecification<Book, Guid>
    {
        public BookNameSpecification(string title) : base(b => b.Title == title)
        {

        }
    }
}
