using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Models.Mediatr.MovieCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.MovieRequests;

namespace Movie_Library_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get All Movies")]
        public async Task<IActionResult> GetAllMovies()
        {
            return Ok(await _mediator.Send(new GetAllMoviesCommand()));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get a Movie")]
        public async Task<IActionResult> GetMovie(int movieId)
        {
            var result = await _mediator.Send(new GetMovieByIdCommand(movieId));
            if (result == null) return NotFound("Movie Doesn't Exist");
            return Ok(result);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Create A Movie")]
        public async Task<IActionResult> CreateMovie([FromBody]AddMovieRequest movie)
        {
            return Ok(await _mediator.Send(new AddMovieCommand(movie)));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("Update A Movie")]
        public async Task<IActionResult> UpdateMovie([FromBody] UpdateMovieRequest movie)
        {
            var result = await _mediator.Send(new GetMovieByIdCommand(movie.MovieId));
            if (result == null) return NotFound("Movie Doesn't Exist");
            return Ok(await _mediator.Send(new UpdateMovieCommand(movie)));
        }
        [HttpDelete("Delete a Movie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int movieId)
        {
            var result = await _mediator.Send(new GetMovieByIdCommand(movieId));
            if (result == null) return NotFound("Movie Doesn't Exist");
            return Ok(await _mediator.Send(new DeleteMovieCommand(movieId)));
        }
    }
}