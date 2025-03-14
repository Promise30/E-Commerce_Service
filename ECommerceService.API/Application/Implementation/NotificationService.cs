using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Events;
using ECommerceService.API.Notifications;
using System.Text;

namespace ECommerceService.API.Application.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;

        public NotificationService(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task UserRegistrationNotification(UserRegisteredEvent userRegisteredEvent)
        {
            EmailMetadata emailMetadata = new
            (
                toAddress: userRegisteredEvent.Email,
                subject: "User Registration",
                body: $"Welcome {userRegisteredEvent.FirstName} {userRegisteredEvent.LastName}, you have successfully registered.\n Kindly click on this link to verify your email {userRegisteredEvent.confirmationLink}."
            );
            await _emailService.Send(emailMetadata);
        }
        public async Task UserEmailVerifiedNotification(UserEmailVerifiedEvent userEmailVerifiedEvent)
        {
            EmailMetadata emailMetadata = new EmailMetadata(
                toAddress: userEmailVerifiedEvent.Email,
                subject: "Email Verification Success",
                body: $"Dear {userEmailVerifiedEvent.FirstName},\n Your email address has been successfully verified. If you did not perform the action, kindly contain our customer service to resolve this");

            await _emailService.Send(emailMetadata);

        }
        public async Task UserTwoFactorTokenNotification(User2FALoginRequestEvent user2FALoginRequestEvent)
        {
            EmailMetadata emailMetadata = new EmailMetadata(
                toAddress: user2FALoginRequestEvent.Email,
                subject: "2FA Login Confirmation",
                body: $"Dear {user2FALoginRequestEvent.UserName},\n We receivced a login request initiated by you. Kindly use this code to continue with your login request. \t {user2FALoginRequestEvent} \t. If you did not initiate" +
                $"this request, kindly disregard this message or reach out to our customer service to resolve this issue.");
            await _emailService.Send(emailMetadata);
        }
        public async Task UserResetPasswordNotification(UserResetPasswordEvent userResetPasswordEvent)
        {
            EmailMetadata emailMetadata = new EmailMetadata(
                toAddress: userResetPasswordEvent.Email,
                subject: "Successful Password Reset",
                body: $"Dear {userResetPasswordEvent.Username},\n Your password has been successfully reset. If you did not perform this action, kindly reach out to our customer service to resolve this issue.");

            await _emailService.Send(emailMetadata);
        }
        public async Task UserForgotPasswordTokenNotification(UserForgotPasswordTokenEvent userForgotPasswordTokenEvent)
        {
            var builder = new StringBuilder();
            var emailBody = builder.AppendLine($"Dear {userForgotPasswordTokenEvent.Username},")
                .AppendLine("You have requested to reset your password.")
                .AppendLine($"Kindly use this token to reset your password. \t {userForgotPasswordTokenEvent.Token} \t.")
                .AppendLine("If you did not perform this action, kindly reach out to our customer service to resolve this issue.")
                .ToString();

            EmailMetadata emailMetadata = new EmailMetadata(
                toAddress: userForgotPasswordTokenEvent.Email,
                subject: "Password Reset Token",
                body: emailBody);

            await _emailService.Send(emailMetadata);
        }

        public async Task User2FAEnableNotification(User2FAEnableEvent user2FAEnableEvent)
        {
            var builder = new StringBuilder();
            var emailBody = builder.AppendLine($"Dear {user2FAEnableEvent.UserName},")
                .AppendLine("You have successfully enabled 2FA on your account. This is a solid approach to further guarantee the safety of your account.")
                .AppendLine("If you did not initiate this request, kindly reach out to our customer service to resolve this issue.")
                .ToString();
            EmailMetadata emailMetadata = new EmailMetadata(
                toAddress: user2FAEnableEvent.Email,
                subject: "Two Factor Authentication Setup Success",
                body: emailBody);
            await _emailService.Send(emailMetadata);
        }
    }
}
