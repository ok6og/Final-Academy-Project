using MediatR;
using MovieLibrary.Models.MongoDbModels;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.WatchedMoviesCommands
{
    public record DeleteWatchedMoviesCommand(int userId) : IRequest<HttpResponse<WatchedList>>
    {
    }
}
