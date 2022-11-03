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
    public class GetAllPlansCommandHandler : IRequestHandler<GetAllPlansCommand,IEnumerable<Plan>>
    {
        private readonly IPlanRepository _planRepository;

        public GetAllPlansCommandHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<IEnumerable<Plan>> Handle(GetAllPlansCommand request, CancellationToken cancellationToken)
        {
            return await _planRepository.GetAllPlans();
        }
    }
}
