using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Events;
using ECommerceService.API.Helpers;
using System.Text;

namespace ECommerceService.API.Application.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;
        private ILogger<NotificationService> _logger;

        public NotificationService(IEmailService emailService, ILogger<NotificationService> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }
        public async Task UserRegistrationNotification(UserRegisteredEvent userRegisteredEvent)
        {
            EmailMetadata emailMetadata = new
            (
                toAddress: userRegisteredEvent.Email,
                subject: "User Registration"
            );
            var templateFilePath = $"{Directory.GetCurrentDirectory()}\\Events\\EmailTemplates\\UserRegistrationEmail.cshtml";
            _logger.LogInformation("Template File Path: {template}", templateFilePath);
           
            await _emailService.Send(emailMetadata, templateFilePath, userRegisteredEvent);
        }
        public async Task UserEmailVerifiedNotification(UserEmailVerifiedEvent userEmailVerifiedEvent)
        {
            EmailMetadata emailMetadata = new EmailMetadata(
                toAddress: userEmailVerifiedEvent.Email,
                subject: "Email Verification Success"
             );
            var templateFilePath = $"{Directory.GetCurrentDirectory()}\\Events\\EmailTemplates\\UserEmailVerified.cshtml";

            await _emailService.Send(emailMetadata, templateFilePath, userEmailVerifiedEvent);

        }
        public async Task UserTwoFactorTokenNotification(User2FALoginRequestEvent user2FALoginRequestEvent)
        {
            EmailMetadata emailMetadata = new EmailMetadata(
                toAddress: user2FALoginRequestEvent.Email,
                subject: "2FA Login Confirmation");

            var templateFilePath = $"{Directory.GetCurrentDirectory()}\\Events\\EmailTemplates\\UserTwoFactorTokenEmail.cshtml";
            await _emailService.Send(emailMetadata, templateFilePath, user2FALoginRequestEvent);
        }
        public async Task UserResetPasswordNotification(UserResetPasswordEvent userResetPasswordEvent)
        {
            EmailMetadata emailMetadata = new EmailMetadata(
                toAddress: userResetPasswordEvent.Email,
                subject: "Successful Password Reset"
                );
            var templateFilePath = $"{Directory.GetCurrentDirectory()}\\Events\\EmailTemplates\\UserResetPasswordEmail.cshtml";
            await _emailService.Send(emailMetadata, templateFilePath, userResetPasswordEvent);
        }
        public async Task UserForgotPasswordTokenNotification(UserForgotPasswordTokenEvent userForgotPasswordTokenEvent)
        {
            var builder = new StringBuilder();
            var emailBody = builder.AppendLine($"Dear {userForgotPasswordTokenEvent.FirstName},")
                .AppendLine("You have requested to reset your password.")
                .AppendLine($"Kindly use this token to reset your password. \t {userForgotPasswordTokenEvent.Token} \t.")
                .AppendLine("If you did not perform this action, kindly reach out to our customer service to resolve this issue.")
                .ToString();

            EmailMetadata emailMetadata = new EmailMetadata(
                toAddress: userForgotPasswordTokenEvent.Email,
                subject: "Password Reset Token");
            var templateFilePath = $"{Directory.GetCurrentDirectory()}\\Events\\EmailTemplates\\UserForgotPasswordTokenEmail.cshtml";
            await _emailService.Send(emailMetadata, templateFilePath, userForgotPasswordTokenEvent);
        }

        public async Task User2FAEnableNotification(User2FAEnableEvent user2FAEnableEvent)
        {
            var builder = new StringBuilder();
            var emailBody = builder.AppendLine($"Dear {user2FAEnableEvent.FirstName},")
                .AppendLine("You have successfully enabled 2FA on your account. This is a solid approach to further guarantee the safety of your account.")
                .AppendLine("If you did not initiate this request, kindly reach out to our customer service to resolve this issue.")
                .ToString();
            EmailMetadata emailMetadata = new EmailMetadata(
                toAddress: user2FAEnableEvent.Email,
                subject: "Two Factor Authentication Setup Success");
            var templateFilePath = $"{Directory.GetCurrentDirectory()}\\Events\\EmailTemplates\\UserTwoFactorEnabledEmail.cshtml";
            await _emailService.Send(emailMetadata, templateFilePath, user2FAEnableEvent);
        }
    }
}
