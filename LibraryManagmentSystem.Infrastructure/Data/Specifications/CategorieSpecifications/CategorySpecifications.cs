using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.QueryParams;
using LibraryManagmentSystem.Shared.SortingOptions;

namespace LibraryManagmentSystem.Infrastructure.Data.Specifications.CategorieSpecifications
{
    class CategorySpecifications : BaseSpecification<Category, Guid>
    {
        public CategorySpecifications(CategoryQueryParams queryParams) :
            base(p => (string.IsNullOrEmpty(queryParams.Name) || p.Name.ToLower().Contains(queryParams.Name.ToLower()))
            && p.IsDeleted == false)
        {
            AddInclude(p => p.Books);
            switch (queryParams.CategorySortingOptions)
            {
                case CategorySortingOptions.nameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case CategorySortingOptions.nameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
            ApplyPagination(queryParams.PageSize, queryParams.pageNumber);
        }
        public CategorySpecifications(string id) : base(p => p.Id.ToString() == id)
        {
            AddInclude(p => p.Books);

        }
    }
}
