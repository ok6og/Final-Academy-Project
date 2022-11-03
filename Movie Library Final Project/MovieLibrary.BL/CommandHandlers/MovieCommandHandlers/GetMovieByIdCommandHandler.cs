using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.MovieCommands;
using MovieLibrary.Models.Models;

namespace MovieLibrary.BL.CommandHandlers.MovieCommandHandlers
{
    public class GetMovieByIdCommandHandler : IRequestHandler<GetMovieByIdCommand, Movie>
    {
        private readonly IMovieRepository _movieRepository;

        public GetMovieByIdCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Movie> Handle(GetMovieByIdCommand request, CancellationToken cancellationToken)
        {
            return await _movieRepository.GetMovieById(request.movieId);
        }
    }
}
