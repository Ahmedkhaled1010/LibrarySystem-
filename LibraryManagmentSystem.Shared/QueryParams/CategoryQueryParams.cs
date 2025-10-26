using LibraryManagmentSystem.Shared.SortingOptions;

namespace LibraryManagmentSystem.Shared.QueryParams
{
    public class CategoryQueryParams : BaseQueryParams
    {
        public string? Name { get; set; }
        public CategorySortingOptions? CategorySortingOptions { get; set; }
    }
}
