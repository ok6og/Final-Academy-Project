using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.SubscriptionCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.SubscriptionCommandHandlers
{
    public class GetSubscriptionByIdCommandHandler : IRequestHandler<GetSubscriptionByIdCommand, HttpResponse<Subscription>>
    {
        private readonly ISubscriptionRepository _subsRepo;
        public GetSubscriptionByIdCommandHandler(ISubscriptionRepository subsrepo)
        {
            _subsRepo = subsrepo;
        }
        public async Task<HttpResponse<Subscription>> Handle(GetSubscriptionByIdCommand request, CancellationToken cancellationToken)
        {
            var sub = await _subsRepo.GetSubscriptionById(request.subId);
            var response = new HttpResponse<Subscription>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully gotten a subscription",
                Value = sub
            };
            if (sub == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "Subscription not found";
            }
            return response;
        }
    }
}
