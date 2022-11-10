using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, HttpResponse<Movie>>
    {
        private readonly IMovieRepository _movieRepository;

        public DeleteMovieCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<HttpResponse<Movie>> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var authorExist = await _movieRepository.DeleteMovie(request.movieId);
            var response = new HttpResponse<Movie>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully deleted a movie",
                Value = authorExist
            };
            if (authorExist == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "Movie does not exist";
            }
            return response;
        }
    }
}
