using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleManagementService _roleManagementService;
        public RolesController(IRoleManagementService roleManagementService)
        {
            _roleManagementService = roleManagementService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var response = await _roleManagementService.GetAllRolesAsync();
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize]
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRole(Guid roleId)
        {
            var response = await _roleManagementService.GetRoleAsync(roleId);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "Admin")]
        [Route("get-users-by-role")]
        [HttpGet]
        public async Task<IActionResult> GetUsersByRole([FromQuery] string roleName)
        {
            var response = await _roleManagementService.GetUsersByRoleAsync(roleName);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "Admin")]
        [Route("add-user-to-role")]
        [HttpPost]
        public async Task<IActionResult> AddUserToRole([FromQuery] Guid userId, string roleName)
        {
            var response = await _roleManagementService.AddUserToRoleAsync(roleName, userId);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "Admin")]
        [Route("remove-user-from-role")]
        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRole([FromQuery] Guid userId, string roleName)
        {
            var response = await _roleManagementService.RemoveUserFromRoleAsync(roleName, userId);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto role)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid data provided");
            }
            var response = await _roleManagementService.CreateRoleAsync(role);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateRole(Guid roleId, [FromBody] UpdateRoleDto role)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid data provided");
            }
            var response = await _roleManagementService.UpdateRoleAsync(roleId, role);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(Guid roleId)
        {
            var response = await _roleManagementService.DeleteRoleAsync(roleId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
