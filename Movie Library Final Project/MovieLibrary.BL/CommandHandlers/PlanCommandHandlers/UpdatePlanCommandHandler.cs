using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.PlanCommands;
using MovieLibrary.Models.Models;

namespace MovieLibrary.BL.CommandHandlers.PlanCommandHandlers
{
    public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand,Plan>
    {
        private IPlanRepository _planRepo;
        private IMapper _mapper;

        public UpdatePlanCommandHandler(IPlanRepository userRepo, IMapper mapper)
        {
            _planRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<Plan> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = _mapper.Map<Plan>(request.plan);
            return await _planRepo.UpdatPlan(plan);
        }
    }
}
