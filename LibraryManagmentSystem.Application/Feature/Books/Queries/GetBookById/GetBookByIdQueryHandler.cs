using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Shared.DataTransferModel.Books;
using LibraryManagmentSystem.Shared.Response;
using MediatR;

namespace LibraryManagmentSystem.Application.Feature.Books.Queries.GetBookById
{
    public class GetBookByIdQueryHandler(IServicesManager servicesManager) : IRequestHandler<GetBookByIdQuery, ApiResponse<BookDto>>
    {
        public async Task<ApiResponse<BookDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetBookByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ApiResponse<BookDto>.Fail("Validation Errors", validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var result = await servicesManager.BookServices.GetBookAsync(request);
            return result;
        }
    }
}
