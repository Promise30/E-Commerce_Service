using AutoMapper;
using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Data.Dtos.Auth;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Domain.Enums;
using ECommerceService.API.Events;
using ECommerceService.API.Helpers;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ECommerceService.API.Application.Implementation
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<UserManagementService> _logger;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        private ApplicationUser? _user;
        public UserManagementService(UserManager<ApplicationUser> userManager, ILogger<UserManagementService> logger, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, INotificationService notificationService)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            
            _notificationService = notificationService;
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
                    _logger.LogError($"--> An error occurred when trying to disable 2FA: {result.Errors.FirstOrDefault()!.Description}");
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
                    _logger.LogError($"--> An error occurred when trying to enable 2FA: {result.Errors.FirstOrDefault()?.Description}");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to enable 2FA", null);
                }
                var user2FAEnableEvent = new User2FAEnableEvent
                {
                    Email = email,
                    FirstName = user.FirstName,
                    DatePublished = DateTime.UtcNow
                };
                BackgroundJob.Enqueue(() => _notificationService.User2FAEnableNotification(user2FAEnableEvent));
                return APIResponse<object>.Create(HttpStatusCode.OK, "2FA enabled successfully", null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to enable 2FA: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to enable 2FA", null);
            }
        }
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

                var userForgotPasswordEvent = new UserForgotPasswordTokenEvent
                {
                    Email = email,
                    FirstName = user.FirstName,
                    DatePublished = DateTime.UtcNow,
                    Token = token
                };
                BackgroundJob.Enqueue(() => _notificationService.UserForgotPasswordTokenNotification(userForgotPasswordEvent));
                return APIResponse<object>.Create(HttpStatusCode.OK, "Password reset token generated successfully", new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to reset user password: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to reset user password", null);
            }
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
                var roles = await _userManager.GetRolesAsync(user);
                userDto.Roles = roles.ToList();
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

        public async Task<APIResponse<object>> LoginUserAsync(LoginUserDto loginUser)
        {
            try
            {
                 _user = await _userManager.FindByEmailAsync(loginUser.Email);
                if (_user == null) {
                    _logger.LogInformation($"User with email {loginUser.Email} does not exist in the system");
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "User not found", null);
                }
                if (!_user.EmailConfirmed)
                {
                    _logger.LogInformation($"User with email {loginUser.Email} has not been verified");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "User email has not been verified", null);
                }

                if (_user.TwoFactorEnabled)
                { 
                    var twoFactorToken = await GenerateOTPForTwoFactor(_user);
                    if (string.IsNullOrEmpty(twoFactorToken))
                    {
                        _logger.LogError($"Failed to generate 2FA token for user {loginUser.Email}");
                        return APIResponse<object>.Create(HttpStatusCode.BadRequest, "Failed to generate 2FA token", null);
                    }
                    _logger.LogInformation($"2FA token generated for user '{loginUser.Email}': {twoFactorToken}");

                    // 2FA Token creation event
                    var userTokenRequest = new User2FALoginRequestEvent
                    {
                        FirstName = _user.FirstName,
                        Email = _user.Email,
                        Token = twoFactorToken
                    };
                    BackgroundJob.Enqueue(() => _notificationService.UserTwoFactorTokenNotification(userTokenRequest));

                    return APIResponse<object>.Create(HttpStatusCode.OK, "2FA token generated successfully", null);
                }
                
                var result = await _userManager.CheckPasswordAsync(_user, loginUser.Password);
                if (!result) {
                    _logger.LogInformation($"User with email {loginUser.Email} entered an incorrect password");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "Incorrect password", null);
                }

                var token = await CreateToken(true);
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };
                _logger.LogInformation("Token generated for user {email}: {token}", _user.Email, JsonSerializer.Serialize(token, options));

                return APIResponse<object>.Create(HttpStatusCode.OK, "User logged in successfully", null);
            }
            catch (Exception ex)
            { 
                _logger.LogError($"An error occurred when trying to login user: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to login user", null);
            }
           
        }
        public async Task<APIResponse<object>> ResendEmailVerificationOTP(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"User with email {email} does not exist in the system");
                return APIResponse<object>.Create(HttpStatusCode.NotFound, "User not found", null);
            }
            if(user.EmailConfirmed)
            {
                _logger.LogInformation($"User with email {email} has already been verified");
                return APIResponse<object>.Create(HttpStatusCode.BadRequest, "User email has already been verified", null);
            }

            var otp = new Random().Next(100000, 999999).ToString();
            user.EmailVerificationCode = otp;
            user.EmailVerificationExpiry = DateTime.UtcNow.AddMinutes(5);
            await _userManager.UpdateAsync(user);

            _logger.LogInformation("Email confirmation otp generated for user {email}: {otp}", user.Email, otp);

            // create event
            var userRegisteredEvent = new UserRegisteredEvent
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateCreated = DateTime.UtcNow,
                ConfirmationCode = otp
            };
            BackgroundJob.Enqueue(() => _notificationService.UserRegistrationNotification(userRegisteredEvent));

            return APIResponse<object>.Create(HttpStatusCode.OK, "Email verification OTP sent successfully", null);
        }
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
                    _logger.LogError($"An error occurred when trying to register new user: {result.Errors.FirstOrDefault()!.Description}");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to register new user", null);
                }


                // add the registered user to the user role
                var role = await _userManager.AddToRoleAsync(user, RoleType.User.GetEnumDescription());
                if(!role.Succeeded) {
                    _logger.LogError($"An error occurred when trying to add user to role: {role.Errors.FirstOrDefault()!.Description}");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to add user to role", null);
                }

                var otp = new Random().Next(100000, 999999).ToString();

                user.EmailVerificationCode = otp;
                user.EmailVerificationExpiry = DateTime.UtcNow.AddMinutes(5);
                await _userManager.UpdateAsync(user);

                _logger.LogInformation("Email confirmation otp generated for user {email}: {otp}", user.Email, otp);

                // create event
                var userRegisteredEvent = new UserRegisteredEvent
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateCreated = DateTime.UtcNow,
                    ConfirmationCode = otp
                };  
                BackgroundJob.Enqueue(() => _notificationService.UserRegistrationNotification(userRegisteredEvent));

                return APIResponse<object>.Create(HttpStatusCode.OK, "User registered successfully", null);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to register new user: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to register new user", null);
            }
        }
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
                    _logger.LogError($"An error occurred when trying to reset user password: {result.Errors.FirstOrDefault().Description}");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to reset user password", null);
                }

                // Password reset event
                var passwordResetEvent = new UserResetPasswordEvent
                {
                    Email = resetPassword.Email,
                    FirstName = user.FirstName,
                    DatePublished = DateTime.UtcNow
                };
                BackgroundJob.Enqueue(() => _notificationService.UserResetPasswordNotification(passwordResetEvent));
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
                if (user == null)
                {
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
                    _logger.LogError($"An error occurred when trying to update user: {result.Errors.FirstOrDefault()!.Description}");
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
        public async Task<APIResponse<TokenDto>> Verify2FA(string email, string token)
        {
            try
            {
                _user = await _userManager.FindByEmailAsync(email);
                if (_user == null) {
                    _logger.LogInformation($"User with email {email} does not exist in the system");
                    return APIResponse<TokenDto>.Create(HttpStatusCode.NotFound, "User not found", null);
                }
                var result = await _userManager.VerifyTwoFactorTokenAsync(_user, "Email", token);
                if (!result) {
                    _logger.LogError($"An error occurred when trying to verify 2FA: {result}");
                    return APIResponse<TokenDto>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to verify 2FA", null);
                }

                var userToken = await CreateToken(false);
                _logger.LogInformation("Token generated for user {email}: {token}", _user.Email, token);
                return APIResponse<TokenDto>.Create(HttpStatusCode.OK, "2FA verified successfully", userToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when trying to verify 2FA: {ex.Message}");
                return APIResponse<TokenDto>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to verify 2FA", null);
            }
        }
        public async Task<APIResponse<object>> VerifyEmailAsync(string email, string token)
        {
            try
            {
                _user = await _userManager.FindByEmailAsync(email);
                if (_user == null) {
                    _logger.LogInformation($"User with email {email} does not exist in the system");
                    return APIResponse<object>.Create(HttpStatusCode.NotFound, "User not found", null);
                }
                if(_user.EmailConfirmed)
                {
                    _logger.LogInformation($"User with email {email} has already been verified");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "User email has already been verified", null);
                }

                // check if otp is valid
                if(_user.EmailVerificationCode != token || _user.EmailVerificationExpiry < DateTime.UtcNow)
                {
                    _logger.LogInformation($"Invalid OTP for user {email}");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "Invalid OTP", null);
                }
                _user.EmailConfirmed = true;
                _user.EmailVerificationCode = null;
                _user.EmailVerificationExpiry = null;
                var result = await _userManager.UpdateAsync(_user);

                if (!result.Succeeded) {
                    _logger.LogError($"An error occurred when trying to verify user email: {result.Errors.FirstOrDefault()!.Description}");
                    return APIResponse<object>.Create(HttpStatusCode.BadRequest, "An error occurred when trying to verify user email", null);
                }

                var userEmailVerifiedEmail = new UserEmailVerifiedEvent
                {
                    Email = email,
                    FirstName = _user.FirstName,
                    DatePublished = DateTime.UtcNow
                };
                BackgroundJob.Enqueue(() =>  _notificationService.UserEmailVerifiedNotification(userEmailVerifiedEmail));

                return APIResponse<object>.Create(HttpStatusCode.OK, "User email verified successfully", null);
            }
            catch (Exception ex)
            {

                _logger.LogError($"An error occurred when trying to verify user email: {ex.Message}");
                return APIResponse<object>.Create(HttpStatusCode.InternalServerError, "An error occurred when trying to verify user email", null);
            }
        }
        public async Task<APIResponse<TokenDto>> RefreshToken(GetNewTokenDto tokenDto)
        {
            try
            {
                var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
                _user = await _userManager.FindByNameAsync(principal.Identity.Name);
                if (_user == null || _user.RefreshToken != tokenDto.RefreshToken || _user.RefreshTokenExpiryDate <= DateTime.Now)
                    return APIResponse<TokenDto>.Create(HttpStatusCode.BadRequest, "Invalid request. The tokenDto has some invalid values.", null);
                var newToken = await CreateToken(populateExp: false);
                return APIResponse<TokenDto>.Create(HttpStatusCode.OK, "Request Successful", newToken);
            }
            catch (Exception ex) 
            {
                _logger.Log(LogLevel.Error, ex.StackTrace);
                _logger.LogInformation($"Error occured while trying to generate new access and refresh token for user: {ex.Message}");
                return APIResponse<TokenDto>.Create(HttpStatusCode.InternalServerError, "Request unsuccessful.", null);
            }
        }

        #region Private methods
        // Configure generation of JWT token
        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var refreshToken = GenerateRefreshToken();
            _user.RefreshToken = refreshToken;
            if (populateExp)
                _user.RefreshTokenExpiryDate = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(_user);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiryDate = tokenOptions.ValidTo,
                RefreshTokenExpiryDate = _user.RefreshTokenExpiryDate
            };
        }
        private SigningCredentials GetSigningCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
             new Claim(ClaimTypes.Name, _user.UserName),
             new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()),
             new Claim(ClaimTypes.Email, _user.Email),
             };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["ValidIssuer"],
            audience: jwtSettings["ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToUInt32(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secretkey"])),
                ValidateLifetime = false,
                ValidIssuer = jwtSettings["ValidIssuer"],
                ValidAudience = jwtSettings["ValidAudience"],
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }

        private async Task<string> GenerateOTPForTwoFactor(ApplicationUser user)
        {
            var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
            if(!providers.Contains("Email"))
            {
                throw new Exception("Email is not a valid two factor provider");
            }
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
            return token;


        }
        #endregion
    }
}
