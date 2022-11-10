using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
