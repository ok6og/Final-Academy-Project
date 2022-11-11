using MovieLibrary.Models.Models;
using MovieLibrary.Models.MongoDbModels;

namespace MovieLibrary.DL.Interfaces
{
    public interface IWatchListRepository
    {
        public Task<Watchlist?> AddWatchList(Watchlist watchlist);
        public Task EmptyWatchList(int userId);
        public Task<IEnumerable<Watchlist>> GetContent(int userId);
        public Task<Movie?> RemoveFromWatchList(int userId, int movieId);
        public Task<Watchlist> GetWatchList(int userId);
        public Task RemoveWatchList(int userId);
    }
}
