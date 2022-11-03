using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.PlanCommands;
using MovieLibrary.Models.Models;

namespace MovieLibrary.BL.CommandHandlers.PlanCommandHandlers
{
    public class GetPlanByIdCommandHandler : IRequestHandler<GetPlanByIdCommand, Plan>
    {
        private readonly IPlanRepository _planRepo;

        public GetPlanByIdCommandHandler(IPlanRepository planRepo)
        {
            _planRepo = planRepo;
        }

        public async Task<Plan> Handle(GetPlanByIdCommand request, CancellationToken cancellationToken)
        {
            return await _planRepo.GetPlanById(request.planId);
        }
    }
}
