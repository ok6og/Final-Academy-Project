using MovieLibrary.Models.Models;

namespace MovieLibrary.Models.MongoDbModels
{
    public class Watchlist
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public IList<Movie> WatchList { get; set; } = new List<Movie>();
    }
}
