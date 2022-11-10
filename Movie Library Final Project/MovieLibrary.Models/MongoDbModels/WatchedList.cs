﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
