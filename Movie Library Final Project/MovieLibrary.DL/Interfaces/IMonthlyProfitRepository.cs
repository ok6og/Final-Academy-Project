using MovieLibrary.Models.Models;

namespace MovieLibrary.DL.Interfaces
{
    public interface IMonthlyProfitRepository
    {
        Task<MonthlyProfit?> AddMonthlyProfit(MonthlyProfit montlyProfit);
        Task<MonthlyProfit?> UpdateProfit(MonthlyProfit montlyProfit);
        Task<bool> IsThereReportAlready(string month, int year);
        Task<MonthlyProfit?> GetMonthlyProfit(int month, int year);
        Task<MonthlyProfit?> IncreaseMonthlyProfit(MonthlyProfit montlyProfit);
    }
}
