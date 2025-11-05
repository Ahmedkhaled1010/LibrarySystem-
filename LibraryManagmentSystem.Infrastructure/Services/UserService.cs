using AutoMapper;
using LibraryManagmentSystem.Application.Feature.Users.Queries.GetUserById;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.UserDto;
using LibraryManagmentSystem.Shared.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class UserService(UserManager<User> userManager, IMapper mapper) : IUserService
    {
        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUser()
        {
            var users = await userManager.Users.ToListAsync();
            var UserDtos = new List<UserDto>();
            foreach (var user in users)
            {
                var role = await userManager.GetRolesAsync(user);
                var dto = mapper.Map<UserDto>(user);
                dto.Role = role.FirstOrDefault();
                UserDtos.Add(dto);
            }
            return ApiResponse<IEnumerable<UserDto>>.Ok(UserDtos);

        }
        public async Task<ApiResponse<UserDto>> GetUserDetailsAsync(GetUserByIdQuery query)
        {
            var user = await userManager.FindByIdAsync(query.userId);
            if (user is null)
            {
                return ApiResponse<UserDto>.Fail("User Not Found");

            }
            var userDto = mapper.Map<UserDto>(user);
            return ApiResponse<UserDto>.Ok(userDto, "User Details");

        }

        public async Task UpdateBorrowLimitAsync(User user, int change)
        {
            user.LimitOfBooksCanBorrow += change;
            await userManager.UpdateAsync(user);
        }

        public async Task<ApiResponse<string>> ValidateUserForBorrowing(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return ApiResponse<string>.Fail("User not found");
            }
            if (user.fines > 0)
            {
                return ApiResponse<string>.Fail("You cannot borrow before paying the fines.");
            }
            if (user.LimitOfBooksCanBorrow <= 0)
            {
                return ApiResponse<string>.Fail("You have exceeded your allowed limit, upgrade your Budget");
            }
            return null;
        }
        public async Task UpdateTotalBorrowAsync(User user)
        {
            user.TotalBorrow += 1;
            await userManager.UpdateAsync(user);
        }


    }
}
