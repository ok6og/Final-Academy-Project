using MediatR;
using MovieLibrary.Models.MongoDbModels;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.WatchListCommands
{
    public record AddMovieToWatchListCommand(int userId, int movieId) : IRequest<HttpResponse<Watchlist>>
    {
    }
}
