using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Models;

namespace MovieLibrary.DL.Repository.MsSqlRepository
{
    public class PlanRepository : IPlanRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PlanRepository> _logger;

        public PlanRepository(ILogger<PlanRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<Plan?> AddPlan(Plan plan)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstAsync<Plan>("INSERT INTO [Plans]  (Type, PricePerMonth) output INSERTED.* VALUES (@Type, @Price)",
                        new { plan.Type, Price = plan.PricePerMonth });
                    _logger.LogInformation("Successfully added a plan");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddPlan)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<Plan?> DeletePlan(int planId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var deletedPlan = await GetPlanById(planId);
                    var result = await conn.ExecuteAsync("DELETE FROM PLANS WHERE PlanId = @Id", new { Id = planId });
                    _logger.LogInformation("Successfully deleted a plan");
                    return deletedPlan;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeletePlan)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<IEnumerable<Plan?>> GetAllPlans()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM PLANS WITH(NOLOCK)";
                    await conn.OpenAsync();
                    _logger.LogInformation("Successfully got all plans");
                    return await conn.QueryAsync<Plan>(query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllPlans)}: {ex.Message}", ex);
            }
            return Enumerable.Empty<Plan>();
        }

        public async Task<Plan?> GetPlanById(int planId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<Plan>("SELECT * FROM PLANS WITH(NOLOCK) WHERE PlanId = @Id", new { Id = planId });
                    _logger.LogInformation("Successfully found a user");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetPlanById)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<Plan?> UpdatPlan(Plan plan)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstAsync<Plan>("UPDATE Plans SET Type = @Type, PricePerMonth = @Price output INSERTED.* WHERE PlanId = @Id",
                        new { plan.Type, Price = plan.PricePerMonth, Id = plan.PlanId });
                    _logger.LogInformation("Successfully updated a plan");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdatPlan)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<int> GetPlanPrice(int planId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<int>("SELECT [PricePerMonth] FROM PLANS WITH(NOLOCK) WHERE PlanId = @Id", new { Id = planId });
                    _logger.LogInformation("Successfully returned money");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetPlanPrice)}: {ex.Message}", ex);
            }
            return 0;
        }
    }
}
