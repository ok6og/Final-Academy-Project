using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.SubscriptionCommands
{
    public record DeleteSubscriptionCommand(int subId) : IRequest<HttpResponse<Subscription>>
    {
    }
}
