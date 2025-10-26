using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Categories.Command.DeleteCategory
{
    public class DeleteCategoryCommandHandler(IServicesManager servicesManager) : IRequestHandler<DeleteCategoryCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ApiResponse<string>.Fail("Validation Failed", errors);
            }
            var response = await servicesManager.CategoryServices.DeleteCategory(request);
            return response;

        }
    }
}
