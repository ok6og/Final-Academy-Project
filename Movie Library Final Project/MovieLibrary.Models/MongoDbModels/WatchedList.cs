using MovieLibrary.Models.Models;

namespace MovieLibrary.Models.MongoDbModels
{
    public record WatchedList
    {
        public Guid Id { get; set; }
        public IList<Movie> WatchedMovies { get; set; } = new List<Movie>();
        public int UserId { get; set; }
        public int TotalTimeSpendInMovies { get; set; }
    }
}
