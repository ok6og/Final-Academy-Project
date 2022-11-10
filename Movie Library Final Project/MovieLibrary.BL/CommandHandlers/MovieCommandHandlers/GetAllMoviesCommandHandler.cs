using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.DL.Repository;
using MovieLibrary.Models.Mediatr.MovieCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.MovieCommandHandlers
{
    public class GetAllMoviesCommandHandler : IRequestHandler<GetAllMoviesCommand, HttpResponse<IEnumerable<Movie>>>
    {
        private readonly IMovieRepository _movieRepository;

        public GetAllMoviesCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<HttpResponse<IEnumerable<Movie>>> Handle(GetAllMoviesCommand request, CancellationToken cancellationToken)
        {
            var movies = await _movieRepository.GetAllMovies();
            var response = new HttpResponse<IEnumerable<Movie>>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully retrieved all movies",
                Value = movies
            };
            if (movies == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = "There are no movies in the database";
            }
            return response;
        }
    }
}
