using MediatR;
using PaymentMicroServices.Domain.Shared.Dtos;
using PaymentMicroServices.Domain.Shared.Response;

namespace PaymentMicroServices.Application.Features.Fines.Command
{
    public class AddFineCommand : IRequest<ApiResponse<FineDto>>
    {
        public string UserId { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Reason { get; set; } = default!;
        public DateTime DateIssued { get; set; }

    }
}
