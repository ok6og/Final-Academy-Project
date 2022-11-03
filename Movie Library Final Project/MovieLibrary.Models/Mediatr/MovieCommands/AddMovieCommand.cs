using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.MovieRequests;

namespace MovieLibrary.Models.Mediatr.MovieCommands
{
    public record AddMovieCommand(AddMovieRequest movie) : IRequest<Movie>
    {
    }
}
