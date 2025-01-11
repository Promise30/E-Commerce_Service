using AutoMapper;
using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data.Dtos;
using ECommerceService.API.Data.Dtos.Auth;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ECommerceService.API.Application.Implementation
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserManagementService> _logger;
        private readonly IMapper _mapper;
        public UserManagementService(UserManager<ApplicationUser> userManager, ILogger<UserManagementService> logger, IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<APIResponse<object>> ChangePasswordAsync(ChangePasswordDto changePassword)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(changePassword.Email);
                if (user == null) {
                    _logger.LogInformation($"--> User with email {changePassword.Email} does not exist in the system");
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "User not found", null);
                }
                var result = await _userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
                if (!result.Succeeded) {
                    _logger.LogError($"--> An error occurred when trying to change user password: {result.Errors}");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to change user password", null);
                }
                return APIResponse<object>.Create(HttpStatusCode.OK, "User password changed successfully", null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to change user password: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to change user password", null);
            }
        }

        public async Task<APIResponse<object>> DeleteUserAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) {
                    _logger.LogInformation($"--> User with email {email} does not exist in the system");
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "User not found", null);
                }
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded) {
                    _logger.LogError($"--> An error occurred when trying to delete user: {result.Errors}");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to delete user", null);
                }
                return APIResponse<object>.Create(HttpStatusCode.OK, "User deleted successfully", null);
            }
            catch (Exception ex)
            {

                _logger.LogError($"An error occurred when trying to delete user: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to delete user", null);
            }
        }

        public async Task<APIResponse<object>> Disable2FA(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) {
                    _logger.LogInformation($"--> User with email {email} does not exist in the system");
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "User not found", null);
                }
                var result = await _userManager.SetTwoFactorEnabledAsync(user, false);
                if (!result.Succeeded) {
                    _logger.LogError($"--> An error occurred when trying to disable 2FA: {result.Errors}");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to disable 2FA", null);
                }
                return APIResponse<object>.Create(HttpStatusCode.OK, "2FA disabled successfully", null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to disable 2FA: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to disable 2FA", null);
            }
        }

        public async Task<APIResponse<object>> Enable2FA(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) {
                    _logger.LogInformation($"--> User with email {email} does not exist in the system");
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "User not found", null);
                }
                var result = await _userManager.SetTwoFactorEnabledAsync(user, true);
                if (!result.Succeeded) {
                    _logger.LogError($"--> An error occurred when trying to enable 2FA: {result.Errors}");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to enable 2FA", null);
                }
                return APIResponse<object>.Create(HttpStatusCode.OK, "2FA enabled successfully", null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to enable 2FA: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to enable 2FA", null);
            }
        }

        // Send email for this method
        public async Task<APIResponse<object>> ForgotPasswordAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) {
                    _logger.LogInformation($"--> User with email {email} does not exist in the system");
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "User not found", null);
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                _logger.LogInformation($"--> Forgot password token generated for user '{email}': {token}");
                return APIResponse<object>.Create(HttpStatusCode.OK, "Password reset token generated successfully", new { Token = token });

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to reset user password: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to reset user password", null);

            }
        }

        public Task<APIResponse<object>> Generate2FAToken(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse<IEnumerable<ApplicationUserDto>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var usersDto = _mapper.Map<IEnumerable<ApplicationUserDto>>(users);
                _logger.LogInformation($"--> {users.Count} users found in the system");
                return APIResponse<IEnumerable<ApplicationUserDto>>.Create(HttpStatusCode.OK, "Users retrieved successfully", usersDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to get all users: {ex.Message}");
                return APIResponse<IEnumerable<ApplicationUserDto>>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to get all users", null);
            }
        }

        public async Task<APIResponse<ApplicationUserDto>> GetUserAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) {
                    _logger.LogInformation($"--> User with email {email} does not exist in the system");
                    return APIResponse<ApplicationUserDto>.Create(HttpStatusCode.NotFound, "User not found", null);
                }
                var userDto = _mapper.Map<ApplicationUserDto>(user);
                _logger.LogInformation($"--> User with email {email} found in the system");
                return APIResponse<ApplicationUserDto>.Create(HttpStatusCode.OK, "User retrieved successfully", userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to get user: {ex.Message}");
                return APIResponse<ApplicationUserDto>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to get user", null);
            }
        }

        public async Task<APIResponse<object>> GetUserRolesAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) {
                    _logger.LogInformation($"--> User with email {email} does not exist in the system");
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "User not found", null);
                }
                var roles = await _userManager.GetRolesAsync(user);
                _logger.LogInformation($"--> User with email {email} has {roles.Count} roles");
                return APIResponse<object>.Create(HttpStatusCode.OK, "User roles retrieved successfully", new { Roles = roles });
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to get user roles: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to get user roles", null);
            }
        }
        // Send email for this method is 2FA enabled
        public async Task<APIResponse<object>> LoginUserAsync(LoginUserDto loginUser)
        {
            try
            {
                 var user = await _userManager.FindByEmailAsync(loginUser.Email);
                if (user == null) {
                    _logger.LogInformation($"User with email {loginUser.Email} does not exist in the system");
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "User not found", null);
                }

                if (user.TwoFactorEnabled)
                {
                    var token = await _userManager.GenerateTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider);
                    _logger.LogInformation($"2FA token generated for user '{loginUser.Email}': {token}");
                    return APIResponse<object>.Create(HttpStatusCode.OK, "2FA token generated successfully", null);
                }

                if (user.LockoutEnabled)
                { 
                    _logger.LogInformation($"User with email {loginUser.Email} is locked out");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "User is locked out", null);
                }
                
                var result = await _userManager.CheckPasswordAsync(user, loginUser.Password);
                if (!result) {
                    _logger.LogInformation($"User with email {loginUser.Email} entered an incorrect password");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "Incorrect password", null);
                }

                return APIResponse<object>.Create(HttpStatusCode.OK, "User logged in successfully", null);
            }
            catch (Exception ex)
            { 
                _logger.LogError($"An error occurred when trying to login user: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to login user", null);
            }
           
        }
        // Send email for this method
        public async Task<APIResponse<object>> RegisterUserAsync(RegisterUserDto registerUser)
        {
            try
            {
                var user = new ApplicationUser
                {
                    FirstName = registerUser.FirstName,
                    LastName = registerUser.LastName,
                    UserName = registerUser.UserName,
                    NormalizedUserName = registerUser.UserName.ToUpper(),
                    NormalizedEmail = registerUser.Email.ToUpper(),
                    Email = registerUser.Email,
                    PhoneCountryCode = registerUser.PhoneCountryCode,
                    PhoneNumber = registerUser.PhoneCountryCode + registerUser.PhoneNumber,
                    DateCreated = DateTimeOffset.UtcNow,
                    LastModifiedDate = DateTimeOffset.UtcNow,
                };

                var result = await _userManager.CreateAsync(user, registerUser.Password);
                if(!result.Succeeded)
                {
                    _logger.LogError($"An error occurred when trying to register new user: {result.Errors}");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to register new user", null);
                }
                return APIResponse<object>.Create(HttpStatusCode.OK, "User registered successfully", null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to register new user: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to register new user", null);
            }
        }
        // Send email for this method
        public async Task<APIResponse<object>> ResetPasswordAsync(ResetPasswordDto resetPassword)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(resetPassword.Email);
                if (user == null) {
                    _logger.LogInformation($"User with email {resetPassword.Email} does not exist in the system");
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "User not found", null);
                }
                var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
                if (!result.Succeeded) {
                    _logger.LogError($"An error occurred when trying to reset user password: {result.Errors}");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to reset user password", null);
                }
                return APIResponse<object>.Create(HttpStatusCode.OK, "User password reset successfully", null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to reset user password: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to reset user password", null);
            }
        }

        public async Task<APIResponse<object>> UpdateUserAsync(UpdateUserDto updateUser)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(updateUser.Email);
                if (user == null) {
                    _logger.LogInformation($"User with email {updateUser.Email} does not exist in the system");
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "User not found", null);
                }
                user.FirstName = updateUser.FirstName;
                user.LastName = updateUser.LastName;
                user.PhoneCountryCode = updateUser.PhoneCountryCode;
                user.PhoneNumber = updateUser.PhoneNumber;
                user.LastModifiedDate = DateTimeOffset.UtcNow;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded) {
                    _logger.LogError($"An error occurred when trying to update user: {result.Errors}");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to update user", null);
                }
                return APIResponse<object>.Create(HttpStatusCode.OK, "User updated successfully", null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to update user: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to update user", null);
            }
        }

        public async Task<APIResponse<object>> Verify2FA(string email, string token)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) {
                    _logger.LogInformation($"User with email {email} does not exist in the system");
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "User not found", null);
                }
                var result = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, token);
                if (!result) {
                    _logger.LogError($"An error occurred when trying to verify 2FA: {result}");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to verify 2FA", null);
                }
                return APIResponse<object>.Create(HttpStatusCode.OK, "2FA verified successfully", null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to verify 2FA: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to verify 2FA", null);
            }
        }

        // Send email for this method
        public async Task<APIResponse<object>> VerifyEmailAsync(string email, string token)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) {
                    _logger.LogInformation($"User with email {email} does not exist in the system");
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "User not found", null);
                }
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (!result.Succeeded) {
                    _logger.LogError($"An error occurred when trying to verify user email: {result.Errors}");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to verify user email", null);
                }
                return APIResponse<object>.Create(HttpStatusCode.OK, "User email verified successfully", null);
            }
            catch (Exception ex)
            {

                _logger.LogError($"An error occurred when trying to verify user email: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to verify user email", null);
            }
        }
    }
}
