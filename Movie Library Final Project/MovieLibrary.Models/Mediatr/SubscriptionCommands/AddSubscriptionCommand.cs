using MediatR;
using MovieLibrary.Models.Requests.SubscriptionRequests;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.SubscriptionCommands
{
    public record AddSubscriptionCommand(AddSubscriptionRequest subscription, int months) : IRequest<HttpResponse<SubscriptionResponse>>
    {
    }
}
