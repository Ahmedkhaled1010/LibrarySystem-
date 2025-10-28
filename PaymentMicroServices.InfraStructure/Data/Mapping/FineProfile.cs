using AutoMapper;
using PaymentMicroServices.Domain.Models;
using PaymentMicroServices.Domain.Shared.Dtos;

namespace PaymentMicroServices.InfraStructure.Data.Mapping
{
    public class FineProfile : Profile
    {
        public FineProfile()
        {

            CreateMap<Fine, FineDto>().ReverseMap();
        }
    }
}
