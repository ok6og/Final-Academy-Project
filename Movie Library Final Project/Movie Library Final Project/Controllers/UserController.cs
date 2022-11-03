using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Models.Mediatr.MovieCommands;
using MovieLibrary.Models.Mediatr.UserCommands;
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
            return Ok(await _mediator.Send(new GetUserByIdCommand(userId)));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Create A User")]
        public async Task<IActionResult> CreateMovie([FromBody] AddUserRequest user)
        {
            return Ok(await _mediator.Send(new AddUserCommand(user)));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("Update A User")]
        public async Task<IActionResult> UpdateMovie([FromBody] UpdateUserRequest user)
        {
            return Ok(await _mediator.Send(new UpdateUserCommand(user)));
        }
        [HttpDelete("Delete a User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int userId)
        {
            return Ok(await _mediator.Send(new DeleteUserCommand(userId)));
        }
    }
}
