using ECommerceService.API.Data.Dtos;
using ECommerceService.API.Helpers;

namespace ECommerceService.API.Application.Interfaces
{
    public interface IRoleManagementService
    {
        Task<APIResponse<IEnumerable<ApplicationRoleDto>>> GetAllRolesAsync();
        Task<APIResponse<ApplicationRoleDto>> GetRoleAsync(string roleId);
        Task<APIResponse<ApplicationRoleDto>> CreateRoleAsync(CreateRoleDto role);
        Task<APIResponse<ApplicationRoleDto>> UpdateRoleAsync(string roleId, UpdateRoleDto role);
        Task<APIResponse<ApplicationRoleDto>> DeleteRoleAsync(string roleId);
        Task<APIResponse<IEnumerable<ApplicationUserDto>>> GetUsersByRoleAsync(string roleName);
        Task<APIResponse<object>> AddUserToRoleAsync(string roleName, string userId);
        Task<APIResponse<object>> RemoveUserFromRoleAsync(string roleName, string userId);
    }
}
