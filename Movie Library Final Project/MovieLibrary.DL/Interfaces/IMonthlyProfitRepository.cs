using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLibrary.Models.Models;

namespace MovieLibrary.DL.Interfaces
{
    public interface IMonthlyProfitRepository
    {
        Task<MonthlyProfit?> AddMonthlyProfit(MonthlyProfit montlyProfit);
        Task<MonthlyProfit?> UpdateProfit(MonthlyProfit montlyProfit);
        Task<bool> IsThereReportAlready();


    }
}
