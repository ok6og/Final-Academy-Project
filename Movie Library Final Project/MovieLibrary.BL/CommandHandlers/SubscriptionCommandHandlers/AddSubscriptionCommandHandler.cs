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

namespace MovieLibrary.BL.CommandHandlers.SubscriptionCommandHandlers
{
    public class AddSubscriptionCommandHandler : IRequestHandler<AddSubscriptionCommand, Subscription>
    {
        private ISubscriptionRepository _subsRepo;
        private IMapper _mapper;

        public AddSubscriptionCommandHandler(ISubscriptionRepository subsrepo, IMapper mapper)
        {
            _subsRepo = subsrepo;
            _mapper = mapper;
        }
        public async Task<Subscription> Handle(AddSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var months = request.months;
            var sub = _mapper.Map<Subscription>(request.subscription);
            return await _subsRepo.AddSubscription(sub,months);
        }
    }
}
