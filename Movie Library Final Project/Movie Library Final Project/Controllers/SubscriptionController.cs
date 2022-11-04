using Kafka.KafkaConfig;
using Kafka.ProducerConsumer.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using MovieLibrary.Models.Mediatr.MovieCommands;
using MovieLibrary.Models.Mediatr.PlanCommands;
using MovieLibrary.Models.Mediatr.SubscriptionCommands;
using MovieLibrary.Models.Mediatr.UserCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.MovieRequests;
using MovieLibrary.Models.Requests.SubscriptionRequests;
using MovieLibrary.Models.Responses;

namespace Movie_Library_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly KafkaProducer<int, SubscriptionResponse> _kafkaProducer;

        public SubscriptionController(IMediator mediator, KafkaProducer<int, SubscriptionResponse> kafkaProducer, IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings)
        {
            _mediator = mediator;
            _kafkaProducer = kafkaProducer;
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
            var subsToReturn = await _mediator.Send(new AddSubscriptionCommand(subs, months));
            _kafkaProducer.Produce(subsToReturn.SubscriptionId, subsToReturn);
            return Ok(subsToReturn);
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
