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
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.MovieCommandHandlers
{
    public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, HttpResponse<Movie>>
    {
        private IMovieRepository _movieRepo;
        private IMapper _mapper;

        public AddMovieCommandHandler(IMovieRepository movieRepo, IMapper mapper)
        {
            _movieRepo = movieRepo;
            _mapper = mapper;
        }

        public async Task<HttpResponse<Movie>> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = _mapper.Map<Movie>(request.movie);
            var result = await _movieRepo.AddMovie(movie);
            var response = new HttpResponse<Movie>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully added a movie",
                Value = result
            };
            if (result == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = "Movie could'not be added";
            }
            return response;
        }
    }
}
