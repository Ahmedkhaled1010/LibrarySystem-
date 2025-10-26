using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.QueryParams;

namespace LibraryManagmentSystem.Infrastructure.Data.Specifications.CategorieSpecifications
{
    class CategoryCountSpecifications : BaseSpecification<Category, Guid>
    {
        public CategoryCountSpecifications(CategoryQueryParams queryParams) :
            base(p => (string.IsNullOrEmpty(queryParams.Name) || p.Name.ToLower().Contains(queryParams.Name.ToLower()))
            && p.IsDeleted == false)
        {

        }
    }
}
