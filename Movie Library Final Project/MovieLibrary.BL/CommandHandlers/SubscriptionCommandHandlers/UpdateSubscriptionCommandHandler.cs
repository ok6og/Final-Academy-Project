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
    public class UpdateSubscriptionCommandHandler : IRequestHandler<UpdateSubscriptionCommand, Subscription>
    {
        private ISubscriptionRepository _subsRepo;
        private IMapper _mapper;

        public UpdateSubscriptionCommandHandler(ISubscriptionRepository subsrepo, IMapper mapper)
        {
            _subsRepo = subsrepo;
            _mapper = mapper;
        }
        public async Task<Subscription> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var sub = _mapper.Map<Subscription>(request.subscription);
            return await _subsRepo.UpdatSubscription(sub);
        }
    }
}
