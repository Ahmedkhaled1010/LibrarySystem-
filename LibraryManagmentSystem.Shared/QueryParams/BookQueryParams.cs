using LibraryManagmentSystem.Shared.SortingOptions;

namespace LibraryManagmentSystem.Shared.QueryParams
{
    public class BookQueryParams : BaseQueryParams
    {
        public string? Title { get; set; } = default!;
        public int? PublishedYear { get; set; }
        public string? AuthorId { get; set; } = default!;
        public bool? IsAvailable { get; set; } = false;
        public string? CategoryName { get; set; } = default!;
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? Price { get; set; }
        public BookSortingOptions? BookSortingOptions { get; set; }
    }
}
