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
            if (request.userId <= 0)
            {
                return new HttpResponse<Watchlist>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = StaticResponses.UserIdLessThanOrEqualTo0,
                    Value = null
                };
            }
            if (request.movieId <= 0)
            {
                return new HttpResponse<Watchlist>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = StaticResponses.MovieIdLessThanOrEqualTo0,
                    Value = null
                };
            }
            var movie = await _movieRepository.GetMovieById(request.movieId);
            var user = await _userRepository.GetUserById(request.userId);
            if (user == null)
            {
                return new HttpResponse<Watchlist>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = StaticResponses.UserDoesNotExist,
                    Value = null
                };
            }
            if (movie == null)
            {
                return new HttpResponse<Watchlist>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = StaticResponses.MovieDoesNotExist,
                    Value = null
                };
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
                return new HttpResponse<Watchlist>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = StaticResponses.SuccessfullyAddedTheObject,
                    Value = watchlistToReturn
                };
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
            return new HttpResponse<Watchlist>
            {
                StatusCode = HttpStatusCode.OK,
                Message = StaticResponses.SuccessfullyAddedTheObject,
                Value = newWatchList
            };
        }
    }
}
