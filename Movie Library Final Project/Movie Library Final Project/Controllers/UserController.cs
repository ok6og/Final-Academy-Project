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
            return Ok(await _mediator.Send(new GetAllUsersCommand()));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get a Users")]
        public async Task<IActionResult> GetUsers(int userId)
        {

            var result = await _mediator.Send(new GetUserByIdCommand(userId));
            if (result.StatusCode== System.Net.HttpStatusCode.OK)
            {
                return StatusCode((int)result.StatusCode, result.Value);
            }
            else
            {
                return StatusCode((int)result.StatusCode, result.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Create A User")]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest user)
        {
            return Ok(await _mediator.Send(new AddUserCommand(user)));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("Update A User")]
        public async Task<IActionResult> UpdateMovie([FromBody] UpdateUserRequest user)
        {
            var result = await _mediator.Send(new GetUserByIdCommand(user.UserId));
            if (result == null) return NotFound("User Does not exist");
            return Ok(await _mediator.Send(new UpdateUserCommand(user)));
        }
        [HttpDelete("Delete a User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int userId)
        {
            var result = await _mediator.Send(new GetUserByIdCommand(userId));
            if (result == null) return NotFound("User Does not exist");
            bool isThereAPlan = await _mediator.Send(new DoesUserHaveSubscriptionCommand(userId));
            if (isThereAPlan) return BadRequest("Cannot remove an user with a subscription");
            return Ok(await _mediator.Send(new DeleteUserCommand(userId)));
        }
    }
}
