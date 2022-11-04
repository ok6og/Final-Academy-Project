using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.SubscriptionCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.SubscriptionCommandHandlers
{
    public class GetSubscriptionByIdCommandHandler : IRequestHandler<GetSubscriptionByIdCommand, SubscriptionResponse>
    {
        private readonly ISubscriptionRepository _subsRepo;
        private readonly IPlanRepository _planRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public GetSubscriptionByIdCommandHandler(ISubscriptionRepository subsrepo, IPlanRepository planRepo, IUserRepository userRepo, IMapper mapper)
        {
            _subsRepo = subsrepo;
            _planRepo = planRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public async Task<SubscriptionResponse> Handle(GetSubscriptionByIdCommand request, CancellationToken cancellationToken)
        {
            var sub = await _subsRepo.GetSubscriptionById(request.subId);
            var subResponse = _mapper.Map<SubscriptionResponse>(sub);
            subResponse.Plan = await _planRepo.GetPlanById(sub.PlanId);
            subResponse.User = await _userRepo.GetUserById(sub.UserId);
            return subResponse;
        }
    }
}
