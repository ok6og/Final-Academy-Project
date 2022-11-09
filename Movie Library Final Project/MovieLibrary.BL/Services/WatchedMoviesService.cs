using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLibrary.BL.Interfaces;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.MongoDbModels;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.Services
{
    public class WatchedMoviesService : IWatchedMoviesService
    {
        private readonly IWatchedMoviesRepository _watchedMoviesRepo;

        public WatchedMoviesService(IWatchedMoviesRepository watchedMoviesRepo)
        {
            _watchedMoviesRepo = watchedMoviesRepo;
        }

        public Task DeleteWatchedMovies(int userId)
        {
            return _watchedMoviesRepo.DeleteWatchedMovie(userId);
        }

        public async Task<HttpResponse<IEnumerable<WatchedList>>> GetWatchedList(int userId)
        {
            var watchedList = await _watchedMoviesRepo.GetWatchedMovies(userId);
            var response = new HttpResponse<IEnumerable<WatchedList>>();
            if (watchedList == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "This user has no watched movies";
            }
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Message = "Succesfully retrieved watched movies for user";
            response.Value = watchedList;
            return response;
        }

        public async Task<HttpResponse<WatchedList?>> SaveWatchedList(WatchedList watchedList)
        {
            var response = new HttpResponse<WatchedList?>();
            var savedWatchedList = await _watchedMoviesRepo.SaveWatchedMovies(watchedList);
            if (savedWatchedList == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = "Couldnt save movie";
            }
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Message = "Succesfully saved movie as watched";
            response.Value = savedWatchedList;
            return response;
        }

    }
}
