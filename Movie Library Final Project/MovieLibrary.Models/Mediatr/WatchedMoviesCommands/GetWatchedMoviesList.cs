using MediatR;
using MovieLibrary.Models.MongoDbModels;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.WatchedMoviesCommands
{
    public record GetWatchedMoviesListCommand(int userId) : IRequest<HttpResponse<IEnumerable<WatchedList>>>
    {
    }
}
