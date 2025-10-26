using AutoMapper;
using LibraryManagmentSystem.Shared.DataTransferModel.Role;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.Application.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleDto>().ReverseMap();

        }
    }
}
