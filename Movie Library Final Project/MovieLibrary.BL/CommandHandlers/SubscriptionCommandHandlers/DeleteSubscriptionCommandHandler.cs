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
    public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, Subscription>
    {
        private ISubscriptionRepository _subsRepo;
        public DeleteSubscriptionCommandHandler(ISubscriptionRepository subsrepo)
        {
            _subsRepo = subsrepo;
        }
        public async Task<Subscription> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
        {
            return await _subsRepo.DeleteSubscription(request.subId);
        }
    }
}
