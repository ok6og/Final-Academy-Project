using AutoMapper;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.PlanCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.PlanCommandHandlers
{
    public class AddPlanCommandHandler : IRequestHandler<AddPlanCommand, HttpResponse<Plan>>
    {
        private IPlanRepository _planRepo;
        private IMapper _mapper;

        public AddPlanCommandHandler(IPlanRepository userRepo, IMapper mapper)
        {
            _planRepo = userRepo;
            _mapper = mapper;
        }
        public async Task<HttpResponse<Plan>> Handle(AddPlanCommand request, CancellationToken cancellationToken)
        {
            var plan = _mapper.Map<Plan>(request.plan);
            var result = await _planRepo.AddPlan(plan);
            var response = new HttpResponse<Plan>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully added a plan",
                Value = result
            };
            if (result == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = "Plan could'not be added";
            }
            return response;
        }
    }
}
