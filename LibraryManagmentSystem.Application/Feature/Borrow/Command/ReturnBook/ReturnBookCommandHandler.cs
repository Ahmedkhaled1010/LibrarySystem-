using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Borrow.Command.ReturnBook
{
    public class ReturnBookCommandHandler(IServicesManager servicesManager) : IRequestHandler<ReturnBookCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var validator = new ReturnBookCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ApiResponse<string>.Fail("Validation Failed", errors);
            }
            var response = await servicesManager.BorrowServices.ReturnBook(request);
            return response;

        }
    }
}
