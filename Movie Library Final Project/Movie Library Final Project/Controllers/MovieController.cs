using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Models.Mediatr.MovieCommands;
using MovieLibrary.Models.Models;

namespace Movie_Library_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;


        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost(Name = "GetWeatherForecast")]
        public async Task<IActionResult> CreateMovie(Movie movie)
        {
            return Ok(await _mediator.Send(new AddMovieCommand(movie)));

        }
    }
}