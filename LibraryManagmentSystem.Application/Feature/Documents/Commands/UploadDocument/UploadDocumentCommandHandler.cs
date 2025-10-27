using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Documents.Commands.UploadDocument
{
    public class UploadDocumentCommandHandler(IServicesManager servicesManager) : IRequestHandler<UploadDocumentCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UploadDocumentCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ApiResponse<string>.Fail("Validation Failed", errors);
            }
            var result = await servicesManager.documentServices.UploadDocumentAsync(request);
            return result;
        }
    }
}
