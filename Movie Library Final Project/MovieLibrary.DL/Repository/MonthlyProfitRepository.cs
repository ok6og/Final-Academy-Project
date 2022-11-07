using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Models;

namespace MovieLibrary.DL.Repository
{
    public class MonthlyProfitRepository : IMonthlyProfitRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MonthlyProfitRepository> _logger;

        public MonthlyProfitRepository(ILogger<MonthlyProfitRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<MonthlyProfit?> AddMonthlyProfit(MonthlyProfit montlyProfit)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstAsync<MonthlyProfit>("INSERT INTO [MonthlyProfit]  (Month, Profit, UserSubscriptionsForMonth,Year) output INSERTED.* VALUES (@Month, @Profit,@UserSubscriptionsForMonth,@Year)",
                        new { Month = montlyProfit.Month, Profit = montlyProfit.Profit, UserSubscriptionsForMonth = montlyProfit.UserSubscriptions, Year = montlyProfit.Year });
                    _logger.LogInformation("Successfully added a MonthlyProfit");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddMonthlyProfit)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<MonthlyProfit?> UpdateProfit(MonthlyProfit montlyProfit)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstAsync<MonthlyProfit>("UPDATE MonthlyProfit SET PROFIT = @Profit, UserSubscriptionsForMonth = @UserSubscriptionsForMonth output INSERTED.* WHERE [Month] = @Month AND Year = @Year",
                        new { Profit = montlyProfit.Profit, UserSubscriptionsForMonth = montlyProfit.UserSubscriptions,Month = montlyProfit.Month, Year = montlyProfit.Year });
                    _logger.LogInformation("Successfully updated profit sheet");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateProfit)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<bool> IsThereReportAlready()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var month = DateTime.Now.ToString("MMMM");
                    var year = DateTime.Now.Year;
                    var report = await conn.QueryAsync<MonthlyProfit>("SELECT * FROM MonthlyProfit WITH(NOLOCK) WHERE [Month] = @Month AND Year = @Year", new { Month = month, Year = year });
                    return report.Any();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(IsThereReportAlready)}: {ex.Message}", ex);
            }
            return false;
        }


    }
}
