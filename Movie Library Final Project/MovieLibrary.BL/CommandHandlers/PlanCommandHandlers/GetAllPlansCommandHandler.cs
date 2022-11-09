using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.DL.Repository;
using MovieLibrary.Models.Mediatr.PlanCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.PlanCommandHandlers
{
    public class GetAllPlansCommandHandler : IRequestHandler<GetAllPlansCommand, HttpResponse<IEnumerable<Plan>>>
    {
        private readonly IPlanRepository _planRepository;

        public GetAllPlansCommandHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<HttpResponse<IEnumerable<Plan>>> Handle(GetAllPlansCommand request, CancellationToken cancellationToken)
        {
            var plans = await _planRepository.GetAllPlans();
            var response = new HttpResponse<IEnumerable<Plan>>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully retrieved all plans",
                Value = plans
            };
            if (plans == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = "There are no plans in the database";
            }
            return response;
        }
    }
}
