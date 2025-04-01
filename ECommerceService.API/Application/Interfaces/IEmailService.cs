using ECommerceService.API.Helpers;

namespace ECommerceService.API.Application.Interfaces
{
    public interface IEmailService
    {
        Task Send(EmailMetadata emailMetadata, string template, object eventType);
        Task Send(EmailMetadata emailMetadata);
    }
}
