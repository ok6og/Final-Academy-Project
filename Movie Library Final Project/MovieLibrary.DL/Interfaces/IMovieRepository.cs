using MovieLibrary.Models.Models;

namespace MovieLibrary.DL.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie?>> GetAllMovies();
        Task<Movie?> GetMovieById(int movieId);
        Task<Movie?> AddMovie(Movie movie);
        Task<Movie?> UpdatMovie(Movie movie);
        Task<Movie?> DeleteMovie(int movieId);
    }
}
