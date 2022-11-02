using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Models;

namespace MovieLibrary.DL.Repository
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
                    var result = await conn.ExecuteScalarAsync("INSERT INTO [Movies] (TITLE, LENGTHINMINUTES, GENRE, RELEASEYEAR) VALUES (@Title, @LengthInMinutes, @Genre, @ReleaseYear)",
                        new { Title = movie.Title, LengthInMinutes = movie.LengthInMinutes, Genre = movie.Genre, ReleaseYear = movie.ReleaseYear });
                    return movie;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddMovie)}: {ex.Message}", ex);
            }
            return null;
        }
    }
}
