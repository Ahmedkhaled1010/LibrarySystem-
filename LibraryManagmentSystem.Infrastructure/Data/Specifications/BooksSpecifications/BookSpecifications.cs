using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.QueryParams;
using LibraryManagmentSystem.Shared.SortingOptions;

namespace LibraryManagmentSystem.Infrastructure.Data.Specifications.BooksSpecifications
{
    class BookSpecifications : BaseSpecification<Book, Guid>
    {
        public BookSpecifications(BookQueryParams queryParams) : base(p =>
    (!queryParams.PublishedYear.HasValue || p.PublishedYear == queryParams.PublishedYear) &&
    (string.IsNullOrEmpty(queryParams.Title) || p.Title.ToLower().Contains(queryParams.Title.ToLower())) &&
    (string.IsNullOrEmpty(queryParams.AuthorName) || p.Author.Name == queryParams.AuthorName) &&
    (string.IsNullOrEmpty(queryParams.CategoryName) || p.Category.Name.ToLower().Contains(queryParams.CategoryName.ToLower())) &&
    (!queryParams.MinPrice.HasValue || p.Price >= queryParams.MinPrice) &&
    (!queryParams.MaxPrice.HasValue || p.Price <= queryParams.MaxPrice) &&
    (!queryParams.Price.HasValue || p.Price == queryParams.Price) && (p.IsAvailableForSale == true)
)

        {
            AddInclude(c => c.Category);
            AddInclude(a => a.Author);

            switch (queryParams.BookSortingOptions)
            {
                case BookSortingOptions.titleAsc:
                    AddOrderBy(p => p.Title);
                    break;
                case BookSortingOptions.titleDesc:
                    AddOrderByDescending(p => p.Title);
                    break;
                case BookSortingOptions.YearAsc:
                    AddOrderBy(p => p.PublishedYear);
                    break;
                case BookSortingOptions.YearDesc:
                    AddOrderByDescending(p => p.PublishedYear);
                    break;
                case BookSortingOptions.TotalBorrowAsc:
                    AddOrderBy(p => p.TotalBorrow);
                    break;
                case BookSortingOptions.TotalBorrowDesc:
                    AddOrderByDescending(p => p.TotalBorrow);
                    break;
                case BookSortingOptions.TotalSellAsc:
                    AddOrderBy(p => p.TotalSell);
                    break;
                case BookSortingOptions.TotalSellDesc:
                    AddOrderByDescending(p => p.TotalSell);
                    break;
                default:
                    break;
            }
            ApplyPagination(queryParams.PageSize, queryParams.pageNumber);

        }
        public BookSpecifications(string Id) : base(b => b.Id.ToString() == Id)
        {
            AddInclude(c => c.Category);
            AddInclude(a => a.Author);

        }
    }
}
