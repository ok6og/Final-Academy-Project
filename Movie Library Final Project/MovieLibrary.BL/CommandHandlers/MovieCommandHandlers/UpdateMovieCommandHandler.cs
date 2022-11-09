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
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.MovieCommandHandlers
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, HttpResponse<Movie>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public UpdateMovieCommandHandler(IMapper mapper, IMovieRepository movieRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
        }

        public async Task<HttpResponse<Movie>> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = _mapper.Map<Movie>(request.movie);
            var result = await _movieRepository.UpdatMovie(movie);
            var response = new HttpResponse<Movie>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully updated a movie",
                Value = result
            };
            if (result == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "There is no movie with such Id";
            }
            return response;
        }
    }
}
