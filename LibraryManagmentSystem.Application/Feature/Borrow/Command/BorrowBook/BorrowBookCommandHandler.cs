using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Borrow.Command.BorrowBook
{
    public class BorrowBookCommandHandler(IServicesManager servicesManager) : IRequestHandler<BorrowBookCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            var validator = new BorrowBookCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ApiResponse<string>.Fail("Validation Failed", errors);
            }
            var result = await servicesManager.BorrowServices.BorrowBook(request);
            return result;
        }
    }
}
