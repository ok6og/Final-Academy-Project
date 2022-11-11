using MovieLibrary.Models.MongoDbModels;

namespace MovieLibrary.DL.Interfaces
{
    public interface IWatchedMoviesRepository
    {
        Task<WatchedList?> SaveWatchedMovies(WatchedList watch);
        Task DeleteWatchedMovie(int userId);
        Task<IEnumerable<WatchedList>> GetWatchedMovies(int userId);
        Task<WatchedList> UpdateWatchedMovies(WatchedList watchedList);
    }
}
