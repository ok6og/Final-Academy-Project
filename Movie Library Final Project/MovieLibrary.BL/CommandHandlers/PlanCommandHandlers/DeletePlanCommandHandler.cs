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
    public class DeletePlanCommandHandler : IRequestHandler<DeletePlanCommand, Plan>
    {
        private readonly IPlanRepository _planRepository;

        public DeletePlanCommandHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Plan> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
        {
            return await _planRepository.DeletePlan(request.planId);
        }
    }
}
