using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Models;

namespace MovieLibrary.DL.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SubscriptionRepository> _logger;

        public SubscriptionRepository(ILogger<SubscriptionRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Subscription?> AddSubscription(Subscription subscription,int months)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var timeNow = DateTime.Now;
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstAsync<Subscription>("INSERT INTO [Subscriptions] (PlanId,UserId, CreatedAt, ValidTill) output INSERTED.* VALUES (@PlanId, @UserId, @CreatedAt, @ValidTill)",
                        new { PlanId = subscription.PlanId, CreatedAt = timeNow, ValidTill = timeNow.AddMonths(months),UserId = subscription.UserId });
                    _logger.LogInformation("Successfully added a subscription");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddSubscription)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<Subscription?> DeleteSubscription(int subscriptionId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var deletedSub = await GetSubscriptionById(subscriptionId);
                    var result = await conn.ExecuteAsync("DELETE FROM SUBSCRIPTIONS WHERE SUBSCRIPTIONID = @Id", new { Id = subscriptionId });
                    _logger.LogInformation("Successfully deleted a subscription");
                    return deletedSub;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteSubscription)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<IEnumerable<Subscription?>> GetAllSubscriptions()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM SUBSCRIPTIONS WITH(NOLOCK)";
                    await conn.OpenAsync();
                    var result = await conn.QueryAsync<Subscription>(query);
                    _logger.LogInformation("Successfully got all subscriptions");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllSubscriptions)}: {ex.Message}", ex);
            }
            return Enumerable.Empty<Subscription>();
        }

        public async Task<Subscription?> GetSubscriptionById(int subscriptionId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<Subscription>("SELECT * FROM SUBSCRIPTIONS WITH(NOLOCK) WHERE SUBSCRIPTIONID = @Id", new { Id = subscriptionId });
                    _logger.LogInformation("Successfully found a subscription");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetSubscriptionById)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<Subscription?> UpdatSubscription(Subscription subscription)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstAsync<Subscription>("UPDATE SUBSCRIPTIONS SET PlanId = @PlanId, UserId = @UserId, CreatedAt = @CreatedAt, ValidTill = @ValidTill output INSERTED.* WHERE SUBSCRIPTIONID = @Id",
                        new { PlanId = subscription.PlanId, UserId = subscription.UserId, CreatedAt = subscription.CreatedAt, ValidTill = subscription.ValidTill, Id = subscription.SubscriptionId });
                    _logger.LogInformation("Successfully updated a subscription");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdatSubscription)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<IEnumerable<Subscription?>> GetAllSubscriptionsForMonth()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM SUBSCRIPTIONS WITH(NOLOCK) WHERE MONTH(CreatedAt) = @Month";
                    await conn.OpenAsync();
                    var result = await conn.QueryAsync<Subscription>(query, new {Month = DateTime.Now.Month});
                    _logger.LogInformation("Successfully got all subscriptions for this month");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllSubscriptionsForMonth)}: {ex.Message}", ex);
            }
            return Enumerable.Empty<Subscription>();
        }
    }
}
