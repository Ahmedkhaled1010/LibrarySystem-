using AutoMapper;
using LibraryManagmentSystem.Shared.DataTransferModel.BasketDto;
using LibraryManagmentSystem.Shared.Model.BasketModule;

namespace LibraryManagmentSystem.Application.Mapping
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
