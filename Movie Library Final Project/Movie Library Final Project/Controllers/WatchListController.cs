using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Models.Mediatr.WatchListCommands;

namespace Movie_Library_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WatchListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WatchListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int userId)
        {
            var response = await _mediator.Send(new GetWatchListCommand(userId));
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }
        [HttpPost("EmptyWatchList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EmptyWatchList(int userId)
        {
            var response = await _mediator.Send(new EmptyWatchListCommand(userId));
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }
        [HttpPost("AddMovieToWatch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMoviesToWatch(int userId, int movieId)
        {
            var response = await _mediator.Send(new AddMovieToWatchListCommand(userId, movieId));
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }
        [HttpDelete("RemoveFromWatchList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveFromWatchList(int userId, int movieId)
        {
            var response = await _mediator.Send(new RemoveMovieFromWatchListCommand(userId, movieId));
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }
        [HttpPost("Fininsh watchlist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FinishWatchList(int userId)
        {
            var response = await _mediator.Send(new FinishWatchListCommand(userId));
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }

    }
}
