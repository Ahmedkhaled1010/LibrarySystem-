using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.Category;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Categories.Command.CreateCategory
{
    public class CreateCategoryCommandHandler(IServicesManager servicesManager) : IRequestHandler<CreateCategoryCommand, ApiResponse<CategoryDto>>
    {

        public async Task<ApiResponse<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validation = new CreateCategoryCommandValidator();
            var validationResult = await validation.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new ApiResponse<CategoryDto>
                {
                    Success = false,
                    Message = "Validation failed",
                    Errors = errors
                };
            }
            var result = await servicesManager.CategoryServices.CreateCategory(request);
            return result;

        }
    }
}
