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
    public class AddPlanCommandHandler : IRequestHandler<AddPlanCommand, Plan>
    {
        private IPlanRepository _planRepo;
        private IMapper _mapper;

        public AddPlanCommandHandler(IPlanRepository userRepo, IMapper mapper)
        {
            _planRepo = userRepo;
            _mapper = mapper;
        }
        public async Task<Plan> Handle(AddPlanCommand request, CancellationToken cancellationToken)
        {
            var plan = _mapper.Map<Plan>(request.plan);
            return await _planRepo.AddPlan(plan);
        }
    }
}
