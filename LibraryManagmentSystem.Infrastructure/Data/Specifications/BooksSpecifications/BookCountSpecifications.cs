using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.QueryParams;

namespace LibraryManagmentSystem.Infrastructure.Data.Specifications.BooksSpecifications
{
    class BookCountSpecifications : BaseSpecification<Book, Guid>
    {
        public BookCountSpecifications(BookQueryParams queryParams) : base(p =>
    (!queryParams.PublishedYear.HasValue || p.PublishedYear == queryParams.PublishedYear) &&
    (string.IsNullOrEmpty(queryParams.Title) || p.Title.ToLower().Contains(queryParams.Title.ToLower())) &&
    (string.IsNullOrEmpty(queryParams.AuthorName) || p.Author.Name == queryParams.AuthorName) &&
    (string.IsNullOrEmpty(queryParams.CategoryName) || p.Category.Name.ToLower().Contains(queryParams.CategoryName.ToLower())) &&
    (!queryParams.MinPrice.HasValue || p.Price >= queryParams.MinPrice) &&
    (!queryParams.MaxPrice.HasValue || p.Price <= queryParams.MaxPrice) &&
    (!queryParams.Price.HasValue || p.Price == queryParams.Price) && (p.IsAvailableForSale == true)
)


        {


        }
    }
}
