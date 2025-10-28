using PaymentMicroServices.Application.Features.Fines.Command;
using PaymentMicroServices.Domain.Shared.Dtos;

namespace PaymentMicroServices.Application.Interfaces
{
    public interface IFineServices
    {
        Task<FineDto> AddFineAsync(AddFineCommand command);
    }
}
