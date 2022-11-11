using Kafka.KafkaConfig;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MovieLibrary.Models.Mediatr.SubscriptionCommands;
using MovieLibrary.Models.Requests.SubscriptionRequests;

namespace Movie_Library_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubscriptionController(IMediator mediator, IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get All Subscriptions")]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            var result = await _mediator.Send(new GetAllSubscriptionsCommand());
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get a Subscription")]
        public async Task<IActionResult> GetSubscription(int subsId)
        {
            var result = await _mediator.Send(new GetSubscriptionByIdCommand(subsId));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Create A Subscription")]
        public async Task<IActionResult> CreateSubscription([FromBody] AddSubscriptionRequest subs, int months)
        {
            var result = await _mediator.Send(new AddSubscriptionCommand(subs, months));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("Update A Subscription")]
        public async Task<IActionResult> UpdateSubscription([FromBody] UpdateSubscriptionRequest subs)
        {
            var result = await _mediator.Send(new UpdateSubscriptionCommand(subs));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
        [HttpDelete("Delete a Subscription")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int subsId)
        {
            var result = await _mediator.Send(new DeleteSubscriptionCommand(subsId));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
    }
}
