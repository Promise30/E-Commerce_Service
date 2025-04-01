using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ECommerceService.API.HealthChecks
{
    public static  class HealthCheck
    {
        public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection")!,
                        name: "SQL Server",
                        healthQuery: "SELECT 1;",
                        failureStatus:HealthStatus.Unhealthy,
                        tags: new[] {"Feedback", "Database"});
            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(15);
                opt.MaximumHistoryEntriesPerEndpoint(60);
                opt.AddHealthCheckEndpoint("ECommerceService API", "/api/health");
            }).AddInMemoryStorage();

        }
    }
}
