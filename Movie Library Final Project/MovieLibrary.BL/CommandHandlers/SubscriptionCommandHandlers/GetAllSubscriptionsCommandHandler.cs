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
    public class GetAllSubscriptionsCommandHandler : IRequestHandler<GetAllSubscriptionsCommand, IEnumerable<Subscription>>
    {
        private ISubscriptionRepository _subsRepo;
        public GetAllSubscriptionsCommandHandler(ISubscriptionRepository subsrepo)
        {
            _subsRepo = subsrepo;
        }
        public async Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsCommand request, CancellationToken cancellationToken)
        {
            return await _subsRepo.GetAllSubscriptions();
        }
    }
}
