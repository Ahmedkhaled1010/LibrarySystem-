using MediatR;
using PaymentMicroServices.Application.Interfaces;
using PaymentMicroServices.Domain.Shared.Dtos;
using PaymentMicroServices.Domain.Shared.Response;

namespace PaymentMicroServices.Application.Features.Fines.Command
{
    public class AddFineCommandHandler(IServicesManager servicesManager) : IRequestHandler<AddFineCommand, ApiResponse<FineDto>>
    {
        public async Task<ApiResponse<FineDto>> Handle(AddFineCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddFineCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ApiResponse<FineDto>.Fail("Validation Failed", errors);
            }
            var result = await servicesManager.FineServices.AddFineAsync(request);
            return ApiResponse<FineDto>.Ok(result, "Fine added successfully.");
        }
    }
}
