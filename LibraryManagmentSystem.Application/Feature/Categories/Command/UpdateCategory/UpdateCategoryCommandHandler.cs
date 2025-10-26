using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Categories.Command.UpdateCategory
{
    public class UpdateCategoryCommandHandler(IServicesManager servicesManager) : IRequestHandler<UpdateCategoryCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ApiResponse<string>.Fail("Validation Errors", errors);
            }
            var result = await servicesManager.CategoryServices.UpdateCategory(request);
            return result;
        }
    }
}
