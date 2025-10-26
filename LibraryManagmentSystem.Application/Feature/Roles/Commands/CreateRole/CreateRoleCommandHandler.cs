using AutoMapper;
using LibraryManagmentSystem.Shared.DataTransferModel.Role;
using LibraryManagmentSystem.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommandHandler(RoleManager<IdentityRole> roleManager,
        IMapper mapper) : IRequestHandler<CreateRoleCommand, ApiResponse<RoleDto>>
    {
        public async Task<ApiResponse<RoleDto>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateRoleCommandValidation();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Any())
            {

                return ApiResponse<RoleDto>.Fail("Validation Failed");

            }
            var role = await roleManager.CreateAsync(new IdentityRole
            {
                Name = request.RoleName
            });

            if (role.Succeeded)
            {
                var createdRole = await roleManager.FindByNameAsync(request.RoleName);
                var createdRoleDto = mapper.Map<IdentityRole, RoleDto>(createdRole!);
                return ApiResponse<RoleDto>.Ok(createdRoleDto, "Role Created Successfully");
            }
            else
            {
                return ApiResponse<RoleDto>.Fail("Role Creation Failed");
            }

        }
    }
}
