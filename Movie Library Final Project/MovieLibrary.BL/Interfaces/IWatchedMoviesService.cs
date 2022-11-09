using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLibrary.Models.MongoDbModels;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.Interfaces
{
    public interface IWatchedMoviesService
    {
        public Task DeleteWatchedMovies(int userId);
        public Task<HttpResponse<IEnumerable<WatchedList>>> GetWatchedList(int userId);
        public Task<HttpResponse<WatchedList?>> SaveWatchedList(WatchedList watchedList);
    }
}
