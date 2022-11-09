using Microsoft.AspNetCore.Mvc;
using MovieLibrary.BL.Interfaces;
using MovieLibrary.DL.Interfaces;

namespace Movie_Library_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WatchListController : ControllerBase
    {
        private readonly IWatchListService _watchListService;

        public WatchListController(IWatchListService watchListService)
        {
            _watchListService = watchListService;
        }

        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int userId)
        {
            var response = await _watchListService.GetContent(userId);
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }
        [HttpPost("EmptyWatchList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EmptyWatchList(int userId)
        {
            await _watchListService.EmptyWatchList(userId);
            return Ok();
        }
        [HttpPost("AddMovie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPurchase(int userId, int movieId)
        {
            var response = await _watchListService.AddToWatchList(userId, movieId);
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }
        [HttpDelete("RemoveFromWatchList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveFromWatchList(int userId, int movieId)
        {
            var response = await _watchListService.RemoveFromWatchList(userId, movieId);
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }
        [HttpPost("AddToWatchedMovies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddToWatchedMovies(int userId)
        {
            var response = await _watchListService.FinishWatchList(userId);
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }

    }
}
