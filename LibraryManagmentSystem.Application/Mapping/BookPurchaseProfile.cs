using AutoMapper;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.Purchases;

namespace LibraryManagmentSystem.Application.Mapping
{
    public class BookPurchaseProfile : Profile
    {
        public BookPurchaseProfile()
        {
            CreateMap<BookPurchase, BookPurchaseDto>().ReverseMap();
        }
    }
}
