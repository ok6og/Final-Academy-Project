using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.PlanCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.PlanCommandHandlers
{
    public class GetPlanByIdCommandHandler : IRequestHandler<GetPlanByIdCommand, HttpResponse<Plan>>
    {
        private readonly IPlanRepository _planRepo;

        public GetPlanByIdCommandHandler(IPlanRepository planRepo)
        {
            _planRepo = planRepo;
        }

        public async Task<HttpResponse<Plan>> Handle(GetPlanByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _planRepo.GetPlanById(request.planId);
            var response = new HttpResponse<Plan>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully gotten a plan",
                Value = result
            };
            if (result == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "Plan not found";
            }
            return response;
        }
    }
}
