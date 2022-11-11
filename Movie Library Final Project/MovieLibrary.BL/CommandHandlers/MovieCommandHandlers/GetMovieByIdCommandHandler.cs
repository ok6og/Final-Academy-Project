using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.MovieCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.MovieCommandHandlers
{
    public class GetMovieByIdCommandHandler : IRequestHandler<GetMovieByIdCommand, HttpResponse<Movie>>
    {
        private readonly IMovieRepository _movieRepository;

        public GetMovieByIdCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<HttpResponse<Movie>> Handle(GetMovieByIdCommand request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetMovieById(request.movieId);
            var response = new HttpResponse<Movie>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully gotten an movie",
                Value = movie
            };
            if (movie == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "Movie not found";
            }
            return response;
        }
    }
}
