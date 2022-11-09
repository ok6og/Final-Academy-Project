using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.MonthlyProficCommands
{
    public record GetMonthlyProfitReportCommand(int month, int year) : IRequest<HttpResponse<MonthlyProfit>>
    {
    }
}
