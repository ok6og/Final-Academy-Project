using System.Net;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.SubscriptionCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.SubscriptionCommandHandlers
{
    public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, HttpResponse<Subscription>>
    {
        private ISubscriptionRepository _subsRepo;
        public DeleteSubscriptionCommandHandler(ISubscriptionRepository subsrepo)
        {
            _subsRepo = subsrepo;
        }
        public async Task<HttpResponse<Subscription>> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subExist = await _subsRepo.DeleteSubscription(request.subId);
            var response = new HttpResponse<Subscription>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully deleted an subscription",
                Value = subExist
            };
            if (subExist == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "Subscription does not exist";
            }
            return response;
        }
    }
}
