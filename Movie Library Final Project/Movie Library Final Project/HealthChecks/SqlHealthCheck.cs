using System.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Movie_Library_Final_Project.HealthChecks
{
    public class SqlHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;

        public SqlHealthCheck(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            await using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);
                }
                catch (SqlException ex)
                {
                    return HealthCheckResult.Unhealthy(ex.Message);
                }
            }
            return HealthCheckResult.Healthy("SQL Connection is OK");
        }
    }
}
