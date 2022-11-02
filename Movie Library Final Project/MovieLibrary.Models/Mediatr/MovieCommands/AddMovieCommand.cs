using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Models.Models;

namespace MovieLibrary.Models.Mediatr.MovieCommands
{
    public record AddMovieCommand(Movie movie) : IRequest<Movie>
    {
    }
}
