using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Models;

namespace MovieLibrary.DL.Repository.MsSqlRepository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MovieRepository> _logger;

        public MovieRepository(ILogger<MovieRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Movie?> AddMovie(Movie movie)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstAsync<Movie>("INSERT INTO [Movies]  (TITLE, LENGTHINMINUTES, GENRE, RELEASEYEAR) output INSERTED.* VALUES (@Title, @LengthInMinutes, @Genre, @ReleaseYear)",
                        new { movie.Title, movie.LengthInMinutes, movie.Genre, movie.ReleaseYear });
                    _logger.LogInformation("Successfully added a movie");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddMovie)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<Movie?> DeleteMovie(int movieId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var deletedBook = await GetMovieById(movieId);
                    var result = await conn.ExecuteAsync("DELETE FROM MOVIES WHERE MovieId = @Id", new { Id = movieId });
                    _logger.LogInformation("Successfully deleted a movie");
                    return deletedBook;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteMovie)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<IEnumerable<Movie?>> GetAllMovies()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM MOVIES WITH(NOLOCK)";
                    await conn.OpenAsync();
                    _logger.LogInformation("Successfully got all movies");
                    return await conn.QueryAsync<Movie>(query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllMovies)}: {ex.Message}", ex);
            }
            return Enumerable.Empty<Movie>();
        }

        public async Task<Movie?> GetMovieById(int movieId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<Movie>("SELECT * FROM MOVIES WITH(NOLOCK) WHERE MovieId = @Id", new { Id = movieId });
                    _logger.LogInformation("Successfully found a movie");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetMovieById)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<Movie?> UpdatMovie(Movie movie)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstAsync<Movie>("UPDATE MOVIES SET TITLE = @Title, LENGTHINMINUTES = @LengthInMinutes, GENRE = @Genre, RELEASEYEAR = @ReleaseYear output INSERTED.* WHERE MOVIEID = @Id",
                        new { movie.Title, movie.LengthInMinutes, movie.Genre, movie.ReleaseYear, Id = movie.MovieId });
                    _logger.LogInformation("Successfully updated a movie");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdatMovie)}: {ex.Message}", ex);
            }
            return null;
        }
    }
}
