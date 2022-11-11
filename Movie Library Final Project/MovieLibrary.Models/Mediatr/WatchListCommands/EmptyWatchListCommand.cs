using MediatR;
using MovieLibrary.Models.MongoDbModels;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.WatchListCommands
{
    public record EmptyWatchListCommand(int userId) : IRequest<HttpResponse<Watchlist>>
    {
    }
}
