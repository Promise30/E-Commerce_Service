using ECommerceService.API.Notifications;

namespace ECommerceService.API.Application.Interfaces
{
    public interface IEmailService
    {
        Task Send(EmailMetadata emailMetadata);
    }
}
