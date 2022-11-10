using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MovieLibrary.Models.MongoDbModels;

namespace Movie_Library_Final_Project.HealthChecks
{
    public class MongoHealthCheck : IHealthCheck
    {
        private IMongoDatabase _db { get; set; }
        public MongoClient _mongoClient { get; set; }
        public MongoHealthCheck(IOptions<MongoDbModel> configuration)
        {
            _mongoClient = new MongoClient(configuration.Value.ConnectionString);
            _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var healthCheckResultHealthy = await CheckMongoDBConnectionAsync();
            if (healthCheckResultHealthy)
            {
                return HealthCheckResult.Healthy("MongoDB health check successfully");
            }
            return HealthCheckResult.Unhealthy("MongoDb health check failed");
        }

        private async Task<bool> CheckMongoDBConnectionAsync()
        {
            try
            {
                await _db.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
            }

            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
