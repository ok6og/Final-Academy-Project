using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.MonthlyProficCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.MonthlyProfitCommandHandlers
{
    public class GetMonthlyProfitReportCommandHandler : IRequestHandler<GetMonthlyProfitReportCommand,HttpResponse<MonthlyProfit>>
    {
        private readonly IMonthlyProfitRepository _monthlyProfitRepository;
        public GetMonthlyProfitReportCommandHandler(IMonthlyProfitRepository monthlyProfitRepository)
        {
            _monthlyProfitRepository = monthlyProfitRepository;
        }
        public async Task<HttpResponse<MonthlyProfit>> Handle(GetMonthlyProfitReportCommand request, CancellationToken cancellationToken)
        {
            var monthlyProfit = await _monthlyProfitRepository.GetMonthlyProfit(request.month, request.year);
            var response = new HttpResponse<MonthlyProfit>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully returned a monthly profit report",
                Value = monthlyProfit
            };
            if (monthlyProfit==null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "Report not found";
            }
            return response;
        }
    }
}
