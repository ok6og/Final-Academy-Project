using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Models.Mediatr.MovieCommands;
using MovieLibrary.Models.Mediatr.SubscriptionCommands;
using MovieLibrary.Models.Requests.MovieRequests;
using MovieLibrary.Models.Requests.SubscriptionRequests;

namespace Movie_Library_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get All Subscriptions")]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            return Ok(await _mediator.Send(new GetAllSubscriptionsCommand()));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get a Subscription")]
        public async Task<IActionResult> GetSubscription(int subsId)
        {
            return Ok(await _mediator.Send(new GetSubscriptionByIdCommand(subsId)));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Create A Subscription")]
        public async Task<IActionResult> CreateSubscription([FromBody] AddSubscriptionRequest subs, int months)
        {
            return Ok(await _mediator.Send(new AddSubscriptionCommand(subs,months)));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("Update A Subscription")]
        public async Task<IActionResult> UpdateSubscription([FromBody] UpdateSubscriptionRequest subs)
        {
            return Ok(await _mediator.Send(new UpdateSubscriptionCommand(subs)));
        }
        [HttpDelete("Delete a Subscription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int subsId)
        {
            return Ok(await _mediator.Send(new DeleteSubscriptionCommand(subsId)));
        }
    }
}
