using System.Net;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.PlanCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.PlanCommandHandlers
{
    public class DeletePlanCommandHandler : IRequestHandler<DeletePlanCommand, HttpResponse<Plan>>
    {
        private readonly IPlanRepository _planRepository;

        public DeletePlanCommandHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<HttpResponse<Plan>> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
        {
            var authorExist = await _planRepository.DeletePlan(request.planId);
            var response = new HttpResponse<Plan>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully deleted a plan",
                Value = authorExist
            };
            if (authorExist == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "Plan does not exist";
            }
            return response;
        }
    }
}
