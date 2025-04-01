using ECommerceService.API.Data.Dtos.Auth;
using ECommerceService.API.Helpers;

namespace ECommerceService.API.Application.Interfaces
{
    public interface IRoleManagementService
    {
        Task<APIResponse<IEnumerable<ApplicationRoleDto>>> GetAllRolesAsync();
        Task<APIResponse<ApplicationRoleDto>> GetRoleAsync(Guid roleId);
        Task<APIResponse<ApplicationRoleDto>> CreateRoleAsync(CreateRoleDto role);
        Task<APIResponse<ApplicationRoleDto>> UpdateRoleAsync(Guid roleId, UpdateRoleDto role);
        Task<APIResponse<ApplicationRoleDto>> DeleteRoleAsync(Guid roleId);
        Task<APIResponse<IEnumerable<ApplicationUserDto>>> GetUsersByRoleAsync(string roleName);
        Task<APIResponse<object>> AddUserToRoleAsync(string roleName, Guid userId);
        Task<APIResponse<object>> RemoveUserFromRoleAsync(string roleName, Guid userId);
    }
}
