using AutoMapper;
using LibraryManagmentSystem.Shared.DataTransferModel.Role;
using LibraryManagmentSystem.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler(RoleManager<IdentityRole> roleManager,
        IMapper mapper) : IRequestHandler<UpdateRoleCommand, ApiResponse<RoleDto>>
    {
        public async Task<ApiResponse<RoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateRoleCommandValidator();
            var result = await validator.ValidateAsync(request);
            if (result.Errors.Any())
            {

                return ApiResponse<RoleDto>.Fail("Validation Failed");

            }
            var role = await roleManager.FindByIdAsync(request.Id);
            if (role == null)
            {

                return ApiResponse<RoleDto>.Fail("Role not found");

            }
            role.Name = request.RoleName;
            var updateResult = await roleManager.UpdateAsync(role);
            if (updateResult.Errors.Any())
            {

                return ApiResponse<RoleDto>.Fail("Role Update Failed");

            }
            var updatedRoleDto = mapper.Map<IdentityRole, RoleDto>(role);
            return ApiResponse<RoleDto>.Ok(updatedRoleDto, "Role Updated Successfully");
        }
    }
}
