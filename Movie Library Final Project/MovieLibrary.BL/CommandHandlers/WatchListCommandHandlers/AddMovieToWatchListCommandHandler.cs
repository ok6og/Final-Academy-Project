using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.WatchListCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.MongoDbModels;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.WatchListCommandHandlers
{
    public class AddMovieToWatchListCommandHandler : IRequestHandler<AddMovieToWatchListCommand, HttpResponse<Watchlist>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IWatchListRepository _watchListRepository;
        public AddMovieToWatchListCommandHandler(IUserRepository userRepository, IMovieRepository movieRepository, IWatchListRepository watchListRepository)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _watchListRepository = watchListRepository;
        }
        public async Task<HttpResponse<Watchlist>> Handle(AddMovieToWatchListCommand request, CancellationToken cancellationToken)
        {
            var response = new HttpResponse<Watchlist>();

            var movie = await _movieRepository.GetMovieById(request.movieId);
            var user = await _userRepository.GetUserById(request.userId);
            if (user == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "There is no such user";
                return response;
            }
            if (movie == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "There is no such movie";
                return response;
            }
            var watchList = await _watchListRepository.GetWatchList(request.userId);
            if (watchList == null)
            {
                Watchlist watchlistToReturn = new Watchlist()
                {
                    UserId = request.userId,
                    Id = Guid.NewGuid(),
                    WatchList = new List<Movie>() { movie }
                };
                await _watchListRepository.AddWatchList(watchlistToReturn);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Added a new watchlist";
                response.Value = watchlistToReturn;
                return response;
            }
            await _watchListRepository.RemoveWatchList(request.userId);
            var movies = watchList.WatchList;
            movies.Add(movie);
            Watchlist newWatchList = new Watchlist()
            {
                UserId = request.userId,
                Id = watchList.Id,
                WatchList = movies
            };
            await _watchListRepository.AddWatchList(newWatchList);
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Added a movie to watchlist";
            response.Value = newWatchList;
            return response;
        }
    }
}
