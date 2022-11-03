using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.MovieCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests;

namespace MovieLibrary.BL.CommandHandlers.MovieCommandHandlers
{
    public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, Movie>
    {
        private IMovieRepository _movieRepo;
        private IMapper _mapper;

        public AddMovieCommandHandler(IMovieRepository movieRepo, IMapper mapper)
        {
            _movieRepo = movieRepo;
            _mapper = mapper;
        }

        public async Task<Movie> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            if (request.movie == null)
            {
                return null;
            }
            var movie = _mapper.Map<Movie>(request.movie);
            return await _movieRepo.AddMovie(movie);
        }
    }
}
