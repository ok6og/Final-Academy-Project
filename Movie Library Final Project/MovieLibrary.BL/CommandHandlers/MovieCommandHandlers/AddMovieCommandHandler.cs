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
    public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, Movie>
    {
        private IMovieRepository _movieRepo;

        public AddMovieCommandHandler(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        public async Task<Movie> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            return await _movieRepo.AddMovie(request.movie);
        }
    }
}
