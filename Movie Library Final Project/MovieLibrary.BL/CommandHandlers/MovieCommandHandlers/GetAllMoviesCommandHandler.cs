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
    public class GetAllMoviesCommandHandler : IRequestHandler<GetAllMoviesCommand, IEnumerable<Movie>>
    {
        private readonly IMovieRepository _movieRepository;

        public GetAllMoviesCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<Movie>> Handle(GetAllMoviesCommand request, CancellationToken cancellationToken)
        {
            return await _movieRepository.GetAllMovies();
        }
    }
}
