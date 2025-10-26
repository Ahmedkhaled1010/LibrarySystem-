using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.Books;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Books.Command.UpdateBook
{
    public class UpdateBookCommandHandler(IServicesManager servicesManager) : IRequestHandler<UpdateBookCommand, ApiResponse<BookDto>>
    {
        public async Task<ApiResponse<BookDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBookCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ApiResponse<BookDto>.Fail("Validation Errors", validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var result = await servicesManager.BookServices.UpdateBookAsync(request);
            return result;
        }
    }
}
