using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.DL.Repository;
using MovieLibrary.Models.Mediatr.SubscriptionCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.SubscriptionCommandHandlers
{
    public class GetAllSubscriptionsCommandHandler : IRequestHandler<GetAllSubscriptionsCommand, HttpResponse<IEnumerable<Subscription>>>
    {
        private ISubscriptionRepository _subsRepo;
        public GetAllSubscriptionsCommandHandler(ISubscriptionRepository subsrepo)
        {
            _subsRepo = subsrepo;
        }
        public async Task<HttpResponse<IEnumerable<Subscription>>> Handle(GetAllSubscriptionsCommand request, CancellationToken cancellationToken)
        {
            var subs = await _subsRepo.GetAllSubscriptions();
            var response = new HttpResponse<IEnumerable<Subscription>>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully retrieved all subscriptions",
                Value = subs
            };
            if (subs == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = "There are no subscriptions in the database";
            }
            return response;
        }
    }
}
