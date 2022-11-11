using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.MovieCommands
{
    public record GetMovieByIdCommand(int movieId) : IRequest<HttpResponse<Movie>>
    {
    }
}
