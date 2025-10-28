using AutoMapper;
using PaymentMicroServices.Application.Features.Fines.Command;
using PaymentMicroServices.Application.Interfaces;
using PaymentMicroServices.Domain.Contracts;
using PaymentMicroServices.Domain.Models;
using PaymentMicroServices.Domain.Shared.Dtos;

namespace PaymentMicroServices.InfraStructure.Services
{
    public class FineServices(IGenericRepository<Fine> repository, IMapper mapper) : IFineServices
    {
        public async Task<FineDto> AddFineAsync(AddFineCommand command)
        {
            var fine = new Fine
            {
                Amount = command.Amount,
                Reason = command.Reason,
                DateIssued = command.DateIssued,

                UserId = command.UserId,
                IsPaid = false
            };
            await repository.AddAsync(fine);
            var fineDto = mapper.Map<FineDto>(fine);
            return fineDto;
        }
    }
}
