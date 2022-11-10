﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.DL.Interfaces;
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
        public async Task<IActionResult> Get(int userId)
        {
            var response = await _mediator.Send(new GetWatchListCommand(userId));
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }
        [HttpPost("EmptyWatchList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EmptyWatchList(int userId)
        {
            var response = await _mediator.Send(new EmptyWatchListCommand(userId));
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }
        [HttpPost("AddMovieToWatch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddMoviesToWatch(int userId, int movieId)
        {
            var response = await _mediator.Send(new AddMovieToWatchListCommand(userId, movieId));
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }
        [HttpDelete("RemoveFromWatchList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveFromWatchList(int userId, int movieId)
        {
            var response = await _mediator.Send(new RemoveMovieFromWatchListCommand(userId, movieId));
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }
        [HttpPost("AddToWatchedMovies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddToWatchedMovies(int userId)
        {
            var response = await _mediator.Send(new FinishWatchListCommand(userId));
            return StatusCode((int)response.StatusCode, new { response.Value, response.Message });
        }

    }
}
