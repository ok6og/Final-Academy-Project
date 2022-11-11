using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.MovieRequests;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.MovieCommands
{
    public record UpdateMovieCommand(UpdateMovieRequest movie) : IRequest<HttpResponse<Movie>>
    {
    }
}
