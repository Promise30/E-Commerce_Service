using ECommerceService.API.Extensions;

namespace ECommerceService.API.Extensions
{
    public static class FluentEmailExtensions
    {
        public static void AddFluentEmail(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings");

            var defaultFromEmail = emailSettings["DefaultFromEmail"];
            var defaultFromName = emailSettings["DefaultFromName"];
            var host = emailSettings["SMTPSetting:Host"];
            var port = emailSettings.GetValue<int>("Port");
            var userName = emailSettings["UserName"];
            var password = emailSettings["Password"];

            services.AddFluentEmail(defaultFromEmail, defaultFromName)
                //.AddSmtpSender(host, port, userName, password);  production
                .AddSmtpSender(host, port)
                .AddRazorRenderer();

        }
    }
}
