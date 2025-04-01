using AutoMapper;
using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data.Dtos.Auth;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ECommerceService.API.Application.Implementation
{
    public class RoleManagementService : IRoleManagementService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RoleManagementService> _logger;
        private readonly IMapper _mapper;
        public RoleManagementService(RoleManager<ApplicationRole> roleManager, ILogger<RoleManagementService> logger, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<APIResponse<object>> AddUserToRoleAsync(string roleName, Guid userId)
        {
            try
            {
                var role = await _roleManager.RoleExistsAsync(roleName);
                if (!role)
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "Role does not exist", null);
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "User does not exist", null);

                // check if user already has the role
                var userHasRole = await _userManager.IsInRoleAsync(user, roleName);
                if (userHasRole) 
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "User already has the role", null);
                // Assign role to user
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded)
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to add user to role", null);
                
                _logger.LogInformation($"User {user.UserName} added to role {roleName}");
                return APIResponse<object>.Create(HttpStatusCode.OK, "User added to role successfully", null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to add user to a role: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to add user to role", null);
            }
        }

        public async Task<APIResponse<ApplicationRoleDto>> CreateRoleAsync(CreateRoleDto role)
        {
            try
            {
                var roleExists = await _roleManager.FindByNameAsync(role.Name);
                if (roleExists != null)
                {
                    return APIResponse<ApplicationRoleDto>.Create(HttpStatusCode.BadRequest, "Role already exists", null);
                }
                var roleToCreate = new ApplicationRole
                {
                    Name = role.Name,
                    NormalizedName = role.Name.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Description = role.Description,
                    DateCreated = DateTimeOffset.UtcNow,
                    LastModifiedDate = DateTimeOffset.UtcNow
                };
                var result = await _roleManager.CreateAsync(roleToCreate);
                if (!result.Succeeded)
                {
                    return APIResponse<ApplicationRoleDto>.Create(HttpStatusCode.BadRequest, "Role creation failed", null);
                }
                _logger.LogInformation($"Role {role.Name} created successfully");
                var roleToReturn = _mapper.Map<ApplicationRoleDto>(roleToCreate);
                return APIResponse<ApplicationRoleDto>.Create(HttpStatusCode.Created, "Role created successfully", roleToReturn);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to create a new role: {ex.Message}");
                return APIResponse<ApplicationRoleDto>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to create a new role", null);
            }
        }

        public async Task<APIResponse<ApplicationRoleDto>> DeleteRoleAsync(Guid roleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role == null)
                {
                    return APIResponse<ApplicationRoleDto>.Create(HttpStatusCode.NotFound, "Role not found", null);
                }
                var result = await _roleManager.DeleteAsync(role);
                if (!result.Succeeded)
                {
                    return APIResponse<ApplicationRoleDto>.Create(HttpStatusCode.BadRequest, "Role deletion failed", null);
                }
                _logger.LogInformation($"Role {role.Name} deleted successfully");
                return APIResponse<ApplicationRoleDto>.Create(HttpStatusCode.OK, "Role deleted successfully", null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to delete a role: {ex.Message}");
                return APIResponse<ApplicationRoleDto>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to delete role", null);
            }

        }

        public async Task<APIResponse<IEnumerable<ApplicationRoleDto>>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _roleManager.Roles.ToListAsync();
                _logger.LogInformation($"{roles.Count} roles retrieved successfully.");
                var rolesToReturn = _mapper.Map<IEnumerable<ApplicationRoleDto>>(roles);
                return APIResponse<IEnumerable<ApplicationRoleDto>>.Create(HttpStatusCode.OK, "Roles retrieved successfully", rolesToReturn);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to retrieve roles: {ex.Message}");
                return APIResponse<IEnumerable<ApplicationRoleDto>>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to retrieve roles", null);

            }
        }

        public async Task<APIResponse<ApplicationRoleDto>> GetRoleAsync(Guid roleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role == null)
                {
                    return APIResponse<ApplicationRoleDto>.Create(HttpStatusCode.NotFound, "Role not found", null);
                }
                _logger.LogInformation($"Role {role.Name} retrieved successfully");
                var roleToReturn = _mapper.Map<ApplicationRoleDto>(role);
                return APIResponse<ApplicationRoleDto>.Create(HttpStatusCode.OK, "Role retrieved successfully", roleToReturn);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to retrieve a role: {ex.Message}");
                return APIResponse<ApplicationRoleDto>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to retrieve role", null);
            }
        }

        public async Task<APIResponse<IEnumerable<ApplicationUserDto>>> GetUsersByRoleAsync(string roleName)
        {
            try
            {
                var role = await _roleManager.RoleExistsAsync(roleName);
                if (!role)
                    return APIResponse<IEnumerable<ApplicationUserDto>>.Create(HttpStatusCode.NotFound, "Role does not exist", null);
                var users = await _userManager.GetUsersInRoleAsync(roleName);
                if (users == null)
                    return APIResponse<IEnumerable<ApplicationUserDto>>.Create(HttpStatusCode.NotFound, "No users found", null);
                _logger.LogInformation($"{users.Count()} users retrieved successfully.");
                var usersToReturn = _mapper.Map<IEnumerable<ApplicationUserDto>>(users);
                // map the role property of each user to rolename
                //usersToReturn.Select(u => u.Roles = roleName);
                return APIResponse<IEnumerable<ApplicationUserDto>>.Create(HttpStatusCode.OK, "Users retrieved successfully", usersToReturn);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to retrieve users: {ex.Message}");
                return APIResponse<IEnumerable<ApplicationUserDto>>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to retrieve users", null);
            }
        }

        public async Task<APIResponse<object>> RemoveUserFromRoleAsync(string roleName, Guid userId)
        {
            try
            {
                var role = await _roleManager.RoleExistsAsync(roleName);
                if (!role)
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "Role does not exist", null);
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "User does not exist", null);

                // check if user has the role
                var userHasRole = await _userManager.IsInRoleAsync(user, roleName);
                if (!userHasRole)
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "User does not have the role", null);
                // Remove role from user
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                if (!result.Succeeded)
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to remove user from role", null);
                
                _logger.LogInformation($"User {user.UserName} removed from role {roleName}");
                return APIResponse<object>.Create(HttpStatusCode.OK, "User removed from role successfully", null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to remove user from role: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to remove user from role", null);
            }
        }

        public async Task<APIResponse<ApplicationRoleDto>> UpdateRoleAsync(Guid roleId, UpdateRoleDto role)
        {
            try
            {
                var roleToUpdate = await _roleManager.FindByIdAsync(roleId.ToString());
                if (roleToUpdate == null)
                {
                    return APIResponse<ApplicationRoleDto>.Create(HttpStatusCode.NotFound, "Role not found", null);
                }
                roleToUpdate.Name = role.Name;
                roleToUpdate.NormalizedName = role.Name.ToUpper();
                roleToUpdate.Description = role.Description;
                roleToUpdate.LastModifiedDate = DateTimeOffset.UtcNow;
                var result = await _roleManager.UpdateAsync(roleToUpdate);
                if (!result.Succeeded)
                {
                    return APIResponse<ApplicationRoleDto>.Create(HttpStatusCode.BadRequest, "Role update failed", null);
                }
                _logger.LogInformation($"Role {role.Name} updated successfully");
                var roleToReturn = _mapper.Map<ApplicationRoleDto>(roleToUpdate);
                return APIResponse<ApplicationRoleDto>.Create(HttpStatusCode.OK, "Role updated successfully", roleToReturn);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to update a role: {ex.Message}");
                return APIResponse<ApplicationRoleDto>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to update role", null);
            }
        }
    }
}
