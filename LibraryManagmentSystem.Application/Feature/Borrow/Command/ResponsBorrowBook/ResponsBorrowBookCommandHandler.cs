using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Borrow.Command.ResponsBorrowBook
{
    public class ResponsBorrowBookCommandHandler(IServicesManager servicesManager) : IRequestHandler<ResponsBorrowBookCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(ResponsBorrowBookCommand request, CancellationToken cancellationToken)
        {
            var validator = new ResponsBorrowBookCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ApiResponse<string>.Fail("Validation Failed", errors);
            }
            return await servicesManager.BorrowServices.ApproveBorrowRequest(request);

        }
    }
}
