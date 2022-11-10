using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Models.Mediatr.WatchedMoviesCommands;

namespace Movie_Library_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WatchedMoviesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WatchedMoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWatchedMovies(int userId)
        {
            var response = await _mediator.Send(new GetWatchedMoviesListCommand(userId));
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteWatchedMovies(int userId)
        {
            var response = await _mediator.Send(new DeleteWatchedMoviesCommand(userId));
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }
    }
}
