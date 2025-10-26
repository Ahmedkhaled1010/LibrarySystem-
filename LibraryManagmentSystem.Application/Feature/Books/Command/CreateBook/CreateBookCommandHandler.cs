using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.Books;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Books.Command.CreateBook
{
    public class CreateBookCommandHandler(IServicesManager servicesManager) : IRequestHandler<CreateBookCommand, ApiResponse<BookDto>>
    {
        public async Task<ApiResponse<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateBookCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ApiResponse<BookDto>.Fail("Validation Failed", errors);
            }
            var result = await servicesManager.BookServices.CreateBookAsync(request);
            return result;
        }
    }
}
