using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data.Dtos;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var response = await _roleManagementService.GetAllRolesAsync();
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRole(string roleId)
        {
            var response = await _roleManagementService.GetRoleAsync(roleId);
            return StatusCode((int)response.StatusCode, response);
        }
        [Route("get-users-by-role")]
        [HttpGet]
        public async Task<IActionResult> GetUsersByRole([FromQuery] string roleName)
        {
            var response = await _roleManagementService.GetUsersByRoleAsync(roleName);
            return StatusCode((int)response.StatusCode, response);
        }
        [Route("add-user-to-role")]
        [HttpGet]
        public async Task<IActionResult> AddUserToRole([FromQuery] string userId, string roleName)
        {
            var response = await _roleManagementService.AddUserToRoleAsync(roleName, userId);
            return StatusCode((int)response.StatusCode, response);
        }
        [Route("remove-user-from-role")]
        [HttpGet]
        public async Task<IActionResult> RemoveUserFromRole([FromQuery] string userId, string roleName)
        {
            var response = await _roleManagementService.RemoveUserFromRoleAsync(roleName, userId);
            return StatusCode((int)response.StatusCode, response);
        }
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
        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateRole(string roleId, [FromBody] UpdateRoleDto role)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid data provided");
            }
            var response = await _roleManagementService.UpdateRoleAsync(roleId, role);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var response = await _roleManagementService.DeleteRoleAsync(roleId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
