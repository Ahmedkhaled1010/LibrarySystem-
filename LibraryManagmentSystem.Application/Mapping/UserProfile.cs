using AutoMapper;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.UserDto;

namespace LibraryManagmentSystem.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ForMember(dist => dist.Role, opt => opt.Ignore());

        }
    }
}
