using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Models.Mediatr.MovieCommands;
using MovieLibrary.Models.Mediatr.PlanCommands;
using MovieLibrary.Models.Mediatr.SubscriptionCommands;
using MovieLibrary.Models.Mediatr.UserCommands;
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
            var subs = await _mediator.Send(new GetSubscriptionByIdCommand(subsId));
            if(subs == null) return NotFound("This subscription does not exist");
            return Ok(subs);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Create A Subscription")]
        public async Task<IActionResult> CreateSubscription([FromBody] AddSubscriptionRequest subs, int months)
        {
            if (subs.PlanId <= 0 || subs.UserId<= 0) return BadRequest("Invalid Id's");
            var user = await _mediator.Send(new GetUserByIdCommand(subs.UserId));
            var plan = await _mediator.Send(new GetPlanByIdCommand(subs.PlanId));
            if (user == null || plan == null) return NotFound("User or plan does not exist");
            return Ok(await _mediator.Send(new AddSubscriptionCommand(subs,months)));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("Update A Subscription")]
        public async Task<IActionResult> UpdateSubscription([FromBody] UpdateSubscriptionRequest subs)
        {
            var subsCheck = await _mediator.Send(new GetSubscriptionByIdCommand(subs.SubscriptionId));
            if (subsCheck == null) return NotFound("This subscription does not exist");
            return Ok(await _mediator.Send(new UpdateSubscriptionCommand(subs)));
        }
        [HttpDelete("Delete a Subscription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int subsId)
        {
            var subsCheck = await _mediator.Send(new GetSubscriptionByIdCommand(subsId));
            if (subsCheck == null) return NotFound("This subscription does not exist");
            return Ok(await _mediator.Send(new DeleteSubscriptionCommand(subsId)));
        }
    }
}
