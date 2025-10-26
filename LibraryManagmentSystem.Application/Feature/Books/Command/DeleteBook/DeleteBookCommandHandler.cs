using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Books.Command.DeleteBook
{
    public class DeleteBookCommandHandler(IServicesManager servicesManager) : IRequestHandler<DeleteBookCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteBookCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ApiResponse<string>.Fail("Validation Errors", validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var result = await servicesManager.BookServices.DeleteBookAsync(request);
            return result;
        }
    }
}
