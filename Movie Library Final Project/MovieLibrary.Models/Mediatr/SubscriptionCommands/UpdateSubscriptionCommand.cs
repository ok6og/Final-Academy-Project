using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.SubscriptionRequests;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.SubscriptionCommands
{
    public record UpdateSubscriptionCommand(UpdateSubscriptionRequest subscription) : IRequest<HttpResponse<Subscription>>
    {
    }
}
