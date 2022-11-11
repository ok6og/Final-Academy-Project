using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.WatchListCommands
{
    public record RemoveMovieFromWatchListCommand(int userId, int movieId) : IRequest<HttpResponse<Movie>>
    {
    }
}
