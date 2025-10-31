using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Documents.Queries.GetDocument
{
    public class GetDocumentQueryHandler(IServicesManager servicesManager) : IRequestHandler<GetDocumentQuery, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetDocumentQueryValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ApiResponse<string>.Fail("Validation errors occurred.", errors);
            }
            var documentUrl = await servicesManager.documentServices.GetDocumentById(request);
            return documentUrl;
        }
    }
}
