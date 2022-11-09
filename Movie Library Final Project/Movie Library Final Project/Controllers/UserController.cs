using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Models.Mediatr.MovieCommands;
using MovieLibrary.Models.Mediatr.UserCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.MovieRequests;
using MovieLibrary.Models.Requests.UserRequests;

namespace Movie_Library_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get All Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result =await _mediator.Send(new GetAllUsersCommand());
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get a Users")]
        public async Task<IActionResult> GetUsers(int userId)
        {
            var result = await _mediator.Send(new GetUserByIdCommand(userId));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Create A User")]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest user)
        {
            var result = await _mediator.Send(new AddUserCommand(user));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });

        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("Update A User")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest user)
        {
            var result = await _mediator.Send(new UpdateUserCommand(user));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
        [HttpDelete("Delete a User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int userId)
        {
            var result =await _mediator.Send(new DeleteUserCommand(userId));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
    }
}
