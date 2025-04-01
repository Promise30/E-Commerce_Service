using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ECommerceService.API.HealthChecks
{
    public class CustomHealthCheck : IHealthCheck
    {
        private readonly string _connectionString;

        public CustomHealthCheck(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _connectionString = connectionString?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);
                    return HealthCheckResult.Healthy("Database is reachable");
                }
                catch(SqlException ex)
                {
                    return HealthCheckResult.Unhealthy("Unable to connect to database", ex);
                }
            }
        }
    }
}
