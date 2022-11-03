using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.SubscriptionCommands;
using MovieLibrary.Models.Models;

namespace MovieLibrary.BL.CommandHandlers.SubscriptionCommandHandlers
{
    public class GetSubscriptionByIdCommandHandler : IRequestHandler<GetSubscriptionByIdCommand, Subscription>
    {
        private ISubscriptionRepository _subsRepo;
        public GetSubscriptionByIdCommandHandler(ISubscriptionRepository subsrepo)
        {
            _subsRepo = subsrepo;
        }
        public async Task<Subscription> Handle(GetSubscriptionByIdCommand request, CancellationToken cancellationToken)
        {
            return await _subsRepo.GetSubscriptionById(request.subId);
        }
    }
}
