using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Documents.Queries.PreviewDocument
{
    public class PreviewDocumentQueryHandler(IServicesManager servicesManager) : IRequestHandler<PreviewDocumentQuery, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(PreviewDocumentQuery request, CancellationToken cancellationToken)
        {
            var validator = new PreviewDocumentQueryValdiator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ApiResponse<string>.Fail("Validation errors occurred.", errors);
            }
            var documentUrl = await servicesManager.documentServices.PreviewDocument(request);
            return documentUrl;
        }
    }
}
