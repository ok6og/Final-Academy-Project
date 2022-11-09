using Microsoft.AspNetCore.Mvc;
using MovieLibrary.BL.Interfaces;
using MovieLibrary.DL.Interfaces;

namespace Movie_Library_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WatchedMoviesController : ControllerBase
    {
        private readonly IWatchedMoviesService _watchedListService;

        public WatchedMoviesController(IWatchedMoviesService watchedListService)
        {
            _watchedListService = watchedListService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWatchedMovies(int userId)
        {
            var response = await _watchedListService.GetWatchedList(userId);
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteWatchedMovies(int userId)
        {
            await _watchedListService.DeleteWatchedMovies(userId);
            return Ok();
        }
    }
}
