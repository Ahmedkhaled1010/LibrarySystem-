using LibraryManagmentSystem.Application.Feature.Categories.Command.CreateCategory;
using LibraryManagmentSystem.Application.Feature.Categories.Command.DeleteCategory;
using LibraryManagmentSystem.Application.Feature.Categories.Command.UpdateCategory;
using LibraryManagmentSystem.Application.Feature.Categories.Queries.GetAllCategories;
using LibraryManagmentSystem.Application.Feature.Categories.Queries.GetCategoryById;
using LibraryManagmentSystem.Shared.DataTransferModel.Category;
using LibraryManagmentSystem.Shared.Response;

namespace LibraryManagmentSystem.Application.Interfaces
{
    public interface ICategoryServices
    {
        Task<ApiResponse<CategoryDto>> CreateCategory(CreateCategoryCommand createCategory);
        Task<PagedApiResponse<CategoryDto>> GetAllCategories(GetAllCategoriesQuery getAllCategories);

        Task<ApiResponse<CategoryDto>> GetCategoryById(GetCategoryByIdQuery getCategoryById);
        Task<ApiResponse<string>> UpdateCategory(UpdateCategoryCommand updateCategory);
        Task<ApiResponse<string>> DeleteCategory(DeleteCategoryCommand updateCategory);

    }
}
