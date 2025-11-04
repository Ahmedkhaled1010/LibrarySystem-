using AutoMapper;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.Borrow;

namespace LibraryManagmentSystem.Application.Mapping
{
    public class BorrowRecordProfile : Profile
    {
        public BorrowRecordProfile()
        {
            CreateMap<BorrowRecord, BorrowRecordDto>().
                ForMember(dist => dist.ReturnDate, opt => opt.MapFrom(src => src.ActualReturnDate))



                .ReverseMap();
        }
    }
}
