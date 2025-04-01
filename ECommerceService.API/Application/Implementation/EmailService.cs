using ECommerceService.API.Application.Interfaces;
using ECommerceService.API.Helpers;
using FluentEmail.Core;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace ECommerceService.API.Application.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }
        public async Task Send(EmailMetadata emailMetadata)
        {
            await _fluentEmail.To(emailMetadata.ToAddress)
                .Subject(emailMetadata.Subject)
                .Body(emailMetadata.Body)
                //.UsingTemplateFromFile(template, eventType)
                .SendAsync();
        }
        public async Task Send(EmailMetadata emailMetadata, string template, object eventType)
        {
            await _fluentEmail.To(emailMetadata.ToAddress)
                .Subject(emailMetadata.Subject)
                //.Body(emailMetadata.Body)
                .UsingTemplateFromFile(template, eventType)
                .SendAsync();
        }
    }
}
