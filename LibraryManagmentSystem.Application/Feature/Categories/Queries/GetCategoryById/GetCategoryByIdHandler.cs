using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.Category;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdHandler(IServicesManager servicesManager) : IRequestHandler<GetCategoryByIdQuery, ApiResponse<CategoryDto>>
    {
        public async Task<ApiResponse<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetCategoryByIdValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ApiResponse<CategoryDto>.Fail("Validation Errors", errors);

            }
            var categoryResult = await servicesManager.CategoryServices.GetCategoryById(request);
            return categoryResult;
        }
    }
}
