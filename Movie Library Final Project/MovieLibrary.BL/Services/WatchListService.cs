using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using MovieLibrary.BL.Interfaces;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.MongoDbModels;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.Services
{
    public class WatchListService : IWatchListService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IWatchListRepository _watchListRepository;
        private readonly IWatchedMoviesRepository _watchedMoviesRepository;
        private readonly IUserRepository _userRepository;
        public WatchListService(IWatchListRepository watchListRepository, IMovieRepository movieRepository, IWatchedMoviesRepository watchedMoviesRepository, IUserRepository userRepository)
        {
            _watchListRepository = watchListRepository;
            _movieRepository = movieRepository;
            _watchedMoviesRepository = watchedMoviesRepository;
            _userRepository = userRepository;
        }

        public async Task<HttpResponse<Watchlist>> AddToWatchList(int userId, int movieId)
        {
            var response = new HttpResponse<Watchlist>();

            var movie = await _movieRepository.GetMovieById(movieId);
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "There is no such user";
                return response;
            }
            var watchList = await _watchListRepository.GetWatchList(userId);
            if (movie == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "There is no such movie";
                return response;
            }

            if (watchList == null)
            {
                Watchlist watchlistToReturn = new Watchlist()
                {
                    UserId = userId,
                    Id = Guid.NewGuid(),
                    WatchList = new List<Movie>() { movie }
                };
                await _watchListRepository.AddWatchList(watchlistToReturn);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Added a new watchlist";
                response.Value = watchlistToReturn;
                return response;
            }
            await _watchListRepository.RemoveWatchList(userId);
            var movies = watchList.WatchList;
            movies.Add(movie);
            Watchlist newWatchList = new Watchlist()
            {
                UserId = userId,
                Id = watchList.Id,
                WatchList = movies
            };
            await _watchListRepository.AddWatchList(newWatchList);
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Added a movie to watchlist";
            response.Value = newWatchList;
            return response;
        }

        public async Task EmptyWatchList(int userId)
        {
            await _watchListRepository.EmptyWatchList(userId);
        }

        public async Task<HttpResponse<WatchedList>> FinishWatchList(int userId)
        {
            var response = new HttpResponse<WatchedList>();
            var finishedWatchList = await _watchListRepository.GetWatchList(userId);
            if (finishedWatchList == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "You cant finish an empty watch list";
                return response;
            }
            var listMovies = await _watchedMoviesRepository.GetWatchedMovies(userId);
            var thisMovies = listMovies.FirstOrDefault();
           
            if (thisMovies == null)
            {
                var watchedList = new WatchedList()
                {
                    Id = Guid.NewGuid(),
                    WatchedMovies = finishedWatchList.WatchList,
                    TotalTimeSpendInMovies = finishedWatchList.WatchList.Sum(x => x.LengthInMinutes),
                    UserId = userId
                };
                await _watchListRepository.RemoveWatchList(userId);
                await _watchListRepository.FinishWatchList(watchedList);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Added a new list of watched movies";
                response.Value = watchedList;
                return response;
            }
            var allMovies = finishedWatchList.WatchList.ToList();
            allMovies.AddRange(thisMovies.WatchedMovies);
            var watchedListAll = new WatchedList()
            {
                Id = thisMovies.Id,
                UserId = userId,
                WatchedMovies = allMovies,
                TotalTimeSpendInMovies = allMovies.Sum(x => x.LengthInMinutes)
            };
            await _watchListRepository.RemoveWatchList(userId);
            await _watchedMoviesRepository.UpdateWatchedMovies(watchedListAll);
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Added a to the list of watched movies";
            response.Value = watchedListAll;
            return response;
        }

        public async Task<HttpResponse<IEnumerable<Watchlist>>> GetContent(int userId)
        {
            var listOfMovies =  await _watchListRepository.GetContent(userId);
            var response = new HttpResponse<IEnumerable<Watchlist>>();
            if (!listOfMovies.Any())
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "There are no movies to be watched";
                return response;
            }
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Returned list of movies to watch";
            response.Value = listOfMovies;
            return response;
        }

        public async Task<HttpResponse<Movie>> RemoveFromWatchList(int userId, int movieId)
        {
            var response = new HttpResponse<Movie>();
            var movie = await _watchListRepository.RemoveFromWatchList(userId, movieId);
            if (movie == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "There is no such movie to be removed from the watch list";
            }
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Successfully removed a movie from watchlist";
            response.Value = movie;
            return response;
        }
    }
}
