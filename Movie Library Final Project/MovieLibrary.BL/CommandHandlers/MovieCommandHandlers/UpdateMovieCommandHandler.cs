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

namespace MovieLibrary.BL.CommandHandlers.MovieCommandHandlers
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Movie>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public UpdateMovieCommandHandler(IMapper mapper, IMovieRepository movieRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
        }

        public async Task<Movie> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = _mapper.Map<Movie>(request.movie);
            return await _movieRepository.UpdatMovie(movie);
        }
    }
}
