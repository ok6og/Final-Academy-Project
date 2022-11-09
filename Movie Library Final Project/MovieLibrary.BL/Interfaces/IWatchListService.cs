using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.MongoDbModels;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.Interfaces
{
    public interface IWatchListService
    {
        public Task<HttpResponse<IEnumerable<Watchlist>>> GetContent(int userId);
        public Task<HttpResponse<Watchlist>> AddToWatchList(int userId, int movieId);
        public Task<HttpResponse<Movie>> RemoveFromWatchList(int userId, int movieId);
        public Task EmptyWatchList(int userId);
        public Task<HttpResponse<WatchedList>> FinishWatchList(int userId);
    }
}
