using ECommerceService.API.Data.Dtos.Auth;
using ECommerceService.API.Helpers;
using System.Threading.Tasks;

namespace ECommerceService.API.Application.Interfaces
{
    public interface IUserManagementService
    {
        Task<APIResponse<object>> RegisterUserAsync(RegisterUserDto registerUser);
        Task<APIResponse<object>> LoginUserAsync(LoginUserDto loginUser);
        Task<APIResponse<object>> VerifyEmailAsync(string email, string token);
        Task<APIResponse<object>> ForgotPasswordAsync(string email);
        Task<APIResponse<object>> ResetPasswordAsync(ResetPasswordDto resetPassword);
        Task<APIResponse<object>> ChangePasswordAsync(ChangePasswordDto changePassword);
        Task<APIResponse<object>> UpdateUserAsync(UpdateUserDto updateUser);
        Task<APIResponse<ApplicationUserDto>> GetUserAsync(string email);
        Task<APIResponse<object>> DeleteUserAsync(string email);
        Task<APIResponse<IEnumerable<ApplicationUserDto>>> GetAllUsersAsync();
        Task<APIResponse<object>> GetUserRolesAsync(string email);
        Task<APIResponse<object>> Enable2FA (string email);
        Task<APIResponse<object>> Disable2FA (string email);
        Task<APIResponse<TokenDto>> Verify2FA (string email, string token);
        Task<APIResponse<TokenDto>> RefreshToken(GetNewTokenDto tokenDto);
        Task<APIResponse<object>> ResendEmailVerificationOTP(string email);
    }
}
