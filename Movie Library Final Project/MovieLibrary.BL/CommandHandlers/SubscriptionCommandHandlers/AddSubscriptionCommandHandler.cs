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
    public class AddSubscriptionCommandHandler : IRequestHandler<AddSubscriptionCommand, SubscriptionResponse>
    {
        private readonly ISubscriptionRepository _subsRepo;
        private readonly IPlanRepository _planRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public AddSubscriptionCommandHandler(ISubscriptionRepository subsrepo, IMapper mapper, IPlanRepository planRepo, IUserRepository userRepo)
        {
            _subsRepo = subsrepo;
            _mapper = mapper;
            _planRepo = planRepo;
            _userRepo = userRepo;
        }
        public async Task<SubscriptionResponse> Handle(AddSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var months = request.months;
            var sub = _mapper.Map<Subscription>(request.subscription);
            var subWithId = await _subsRepo.AddSubscription(sub,months);

            var subResponse = _mapper.Map<SubscriptionResponse>(subWithId);
            subResponse.Plan =await _planRepo.GetPlanById(sub.PlanId);
            subResponse.User = await _userRepo.GetUserById(sub.UserId);
            return subResponse;
        }
    }
}
