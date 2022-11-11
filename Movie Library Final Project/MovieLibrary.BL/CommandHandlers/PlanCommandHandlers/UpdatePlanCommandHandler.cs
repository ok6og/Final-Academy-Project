using AutoMapper;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.PlanCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.PlanCommandHandlers
{
    public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand, HttpResponse<Plan>>
    {
        private IPlanRepository _planRepo;
        private IMapper _mapper;

        public UpdatePlanCommandHandler(IPlanRepository userRepo, IMapper mapper)
        {
            _planRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<HttpResponse<Plan>> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = _mapper.Map<Plan>(request.plan);
            var result = await _planRepo.UpdatPlan(plan);
            var response = new HttpResponse<Plan>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully updated a plan",
                Value = result
            };
            if (result == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "There is no plan with such Id";
            }
            return response;
        }
    }
}
