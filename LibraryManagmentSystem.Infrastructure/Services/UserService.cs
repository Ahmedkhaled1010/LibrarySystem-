using AutoMapper;
using LibraryManagmentSystem.Application.Feature.Auth.Register;
using LibraryManagmentSystem.Application.Feature.Users.Command.ChangePassword;
using LibraryManagmentSystem.Application.Feature.Users.Command.DeleteUser;
using LibraryManagmentSystem.Application.Feature.Users.Command.NewFolder;
using LibraryManagmentSystem.Application.Feature.Users.Command.UploadProfileImage;
using LibraryManagmentSystem.Application.Feature.Users.Queries.GetUserById;
using LibraryManagmentSystem.Application.IClients;
using LibraryManagmentSystem.Application.Interfaces;
using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Shared.DataTransferModel.Auth;
using LibraryManagmentSystem.Shared.DataTransferModel.UserDto;
using LibraryManagmentSystem.Shared.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Infrastructure.Services
{
    public class UserService(UserManager<User> userManager, IMapper mapper,
         ISupabaseClient supabase,IUnitOfWork unitOfWork) : IUserService
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

        public async Task<ApiResponse<UserDto>> UpdateUserDetailsAsync(UpdateUserCommand query)
        {
            var user = await userManager.FindByIdAsync(query.UserId);
            if (user is null)
            {
                return ApiResponse<UserDto>.Fail("User Not Found");
            }
            UpdateUser(query, user);
            await userManager.UpdateAsync(user);
            var userDto = mapper.Map<UserDto>(user);
            return ApiResponse<UserDto>.Ok(userDto, "User Updated Successfuly");

        }
        public async Task<ApiResponse<string>> ChangePasswordAsync(ChangePasswordCommand command)
        {
            var user = await userManager.FindByIdAsync(command.UserId);
            if (user is null)
            {
                return ApiResponse<string>.Fail("User Not Found");
            }
            var result = await userManager.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);

            if (!result.Succeeded)
                return ApiResponse<string>.Fail("Change Password failed", result.Errors.Select(x => x.Description).ToList());

            return ApiResponse<string>.Ok("Password changed successfully");


        }
        public async Task<ApiResponse<string>> UploadProfileIamge(UploadProfileImageCommand command)
        {
            var user = await userManager.FindByIdAsync(command.UserId);
            if (user is null)
            {
                return ApiResponse<string>.Fail("User Not Found");
            }
            var fileExtension = Path.GetExtension(command.file.FileName).ToLowerInvariant();
            var fileName = $"{Guid.NewGuid()}_{command.file.FileName}";
            using var stream = command.file.OpenReadStream();
            var publicUrl = await supabase.UploadFileAsync("Profile", fileName, stream);
            user.ImagePath = publicUrl;
            await userManager.UpdateAsync(user);
            return ApiResponse<string>.Ok("Uploade Profile Image Successfuly");

        }
        private static void UpdateUser(UpdateUserCommand query, User? user)
        {
            if (query.Name is not null)
            {
                user.Name = query.Name;

            }
            if (query.PhoneNumber is not null)
            {
                user.PhoneNumber = query.PhoneNumber;

            }
        }

        public async Task<int> GetTotalUserAsync()
        {
            return await userManager.Users.CountAsync();
        }

        public async Task<ApiResponse<string>> DeleteUserAsync(DeleteUserCommand command)
        {
            var user = await userManager.FindByIdAsync(command.UserId);
            await userManager.DeleteAsync(user);
            return ApiResponse<string>.Ok("User Deleted Success");
        }

        public async Task<UserDto> GetUserById(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return mapper.Map<UserDto>(user);
        }

        public async Task<User> CreateUserAsync(RegisterCommand registerDto,string token)
        {
            User user = new User
            {
                Email = registerDto.EmailAddress,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
                Name = registerDto.Name
            };
            using var transaction = await unitOfWork.BeginTransactionAsync();
            user.verificationToken = token;
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                await userManager.UpdateAsync(user);
                await transaction.CommitAsync();
                return user;

            }
            else
            {
                await transaction.RollbackAsync();
                return null;
            }
           

        }

        public async Task<AuthDto> AuthUser(User user, TokenModel token, RefreshToken refreshTokens)
        {
            var User = new AuthDto()
            {
                Email = user.Email,
                IsAuthenticated = true,
                Name = user.UserName,
                Token = token.Token,
                Roles = token.Roles,
                IsVerified = user.IsVerified
            };
            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var refreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                User.RefreshToken = refreshToken.Token;
                User.RefreshTokenExpiryTime = refreshToken.ExpiresOn;


            }
            else
            {

                var refreshToken = refreshTokens;
                User.RefreshToken = refreshToken.Token;
                User.RefreshTokenExpiryTime = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                await userManager.UpdateAsync(user);
            }
            return User;
        }
    }
}
