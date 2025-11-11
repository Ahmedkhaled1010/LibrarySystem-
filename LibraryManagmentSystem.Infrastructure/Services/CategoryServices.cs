using AutoMapper;
using LibraryManagmentSystem.Application.Feature.Categories.Command.CreateCategory;
using LibraryManagmentSystem.Application.Feature.Categories.Command.DeleteCategory;
using LibraryManagmentSystem.Application.Feature.Categories.Command.UpdateCategory;
using LibraryManagmentSystem.Application.Feature.Categories.Queries.GetAllCategories;
using LibraryManagmentSystem.Application.Feature.Categories.Queries.GetCategoryById;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Infrastructure.Data.Specifications.CategorieSpecifications;
using LibraryManagmentSystem.Shared.DataTransferModel.Category;
using LibraryManagmentSystem.Shared.Response;
using System.Net;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class CategoryServices(IUnitOfWork unitOfWork, IMapper mapper) : ICategoryServices
    {
        private readonly IGenericRepository<Category, Guid> categoryRepository = unitOfWork.GetRepository<Category, Guid>();

        public async Task<ApiResponse<CategoryDto>> CreateCategory(CreateCategoryCommand createCategory)
        {
            try
            {
                var category = new Category
                {
                    Name = createCategory.Name,
                    Description = createCategory.Description
                };
                await categoryRepository.AddAsync(category);
                await unitOfWork.SaveChangesAsync();
                var categoryDto = mapper.Map<CategoryDto>(category);
                return ApiResponse<CategoryDto>.Ok(categoryDto, "Category created successfully");
            }
            catch (Exception ex) {
                return ApiResponse<CategoryDto>.Fail($"Error: {ex.InnerException?.Message ?? ex.Message}");


            }
        }



        public async Task<PagedApiResponse<CategoryDto>> GetAllCategories(GetAllCategoriesQuery getAllCategories)
        {
           try
            {
                var specification = new CategorySpecifications(getAllCategories.CategoryQueryParams);
                var categories = await categoryRepository.GetAllAsync(specification);
                if (categories is null)
                {
                    return PagedApiResponse<CategoryDto>.Fail("No categories found.",
        (int)HttpStatusCode.NotFound);
                }
                var countSpecification = new CategoryCountSpecifications(getAllCategories.CategoryQueryParams);
                var totalItems = await categoryRepository.CountAsync(countSpecification);
                var categoryDtos = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);
                PaginationInfo pagination = new PaginationInfo
                {
                    PageNumber = getAllCategories.CategoryQueryParams.pageNumber,
                    PageSize = getAllCategories.CategoryQueryParams.PageSize,
                    TotalRecords = totalItems
                };
                return PagedApiResponse<CategoryDto>.Ok(categoryDtos.ToList(), pagination, "Categories retrieved successfully");
            }
            catch (Exception ex)
            {
                return PagedApiResponse<CategoryDto>.Fail($"Error: {ex.InnerException?.Message ?? ex.Message}");


            }
        }

        public async Task<ApiResponse<CategoryDto>> GetCategoryById(GetCategoryByIdQuery getCategoryById)
        {
            try
            {
                var specification = new CategorySpecifications(getCategoryById.Id.ToString());
                var category = await categoryRepository.GetByIdAsync(specification);
                if (category == null)
                {
                    return ApiResponse<CategoryDto>.Fail("Category not found", (int)HttpStatusCode.NotFound);
                }
                var categoryDto = mapper.Map<CategoryDto>(category);
                return ApiResponse<CategoryDto>.Ok(categoryDto, "Category retrieved successfully");
            }
            catch (Exception ex) {
                return ApiResponse<CategoryDto>.Fail($"Error: {ex.InnerException?.Message ?? ex.Message}");

            }
        }

        public async Task<ApiResponse<string>> UpdateCategory(UpdateCategoryCommand updateCategory)
        {
            try
            {
                var category = await categoryRepository.GetByIdAsync(Guid.Parse(updateCategory.Id));
                if (category == null)
                {
                    return ApiResponse<string>.Fail("Category not found");
                }
                if (!string.Equals(category.Name, updateCategory.Name, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(updateCategory.Name))
                {
                    category.Name = updateCategory.Name;
                }
                if (!string.Equals(category.Description, updateCategory.Description, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(updateCategory.Description))
                {
                    category.Description = updateCategory.Description;
                }
                categoryRepository.Update(category);
                await unitOfWork.SaveChangesAsync();
                return ApiResponse<string>.Ok($"Category {category.Name}", "Category updated successfully");
            }
            catch (Exception ex) {
                return ApiResponse<string>.Fail($"Error: {ex.InnerException?.Message ?? ex.Message}");

            }
        }
        public async Task<ApiResponse<string>> DeleteCategory(DeleteCategoryCommand updateCategory)
        {
            try
            {
                var category = await categoryRepository.GetByIdAsync(Guid.Parse(updateCategory.Id));
                if (category == null)
                {
                    return ApiResponse<string>.Fail("Category not found");
                }
                category.IsDeleted = true;
                categoryRepository.Update(category);
                await unitOfWork.SaveChangesAsync();
                return ApiResponse<string>.Ok($"Category {category.Name}", "Category deleted successfully");
            }
            catch (Exception ex) {
                return ApiResponse<string>.Fail($"Error: {ex.InnerException?.Message ?? ex.Message}");

            }
        }
    }
}
