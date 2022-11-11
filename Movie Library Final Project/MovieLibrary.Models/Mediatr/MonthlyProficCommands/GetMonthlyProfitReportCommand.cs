using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.MonthlyProficCommands
{
    public record GetMonthlyProfitReportCommand(int month, int year) : IRequest<HttpResponse<MonthlyProfit>>
    {
    }
}
