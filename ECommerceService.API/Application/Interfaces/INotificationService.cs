using ECommerceService.API.Events;

namespace ECommerceService.API.Application.Interfaces
{
    public interface INotificationService
    {
        Task UserEmailVerifiedNotification(UserEmailVerifiedEvent userEmailVerifiedEvent);
        Task UserForgotPasswordTokenNotification(UserForgotPasswordTokenEvent userForgotPasswordTokenEvent);
        Task UserRegistrationNotification(UserRegisteredEvent userRegisteredEvent);
        Task UserResetPasswordNotification(UserResetPasswordEvent userResetPasswordEvent);
        Task UserTwoFactorTokenNotification(User2FALoginRequestEvent user2FALoginRequestEvent);
        Task User2FAEnableNotification(User2FAEnableEvent user2FAEnableEvent);
    }
}
