using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Models.Mediatr.PlanCommands;
using MovieLibrary.Models.Mediatr.UserCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.PlanRequests;
using MovieLibrary.Models.Requests.UserRequests;

namespace Movie_Library_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get All Plans")]
        public async Task<IActionResult> GetAllPlans()
        {
            var result = await _mediator.Send(new GetAllPlansCommand());
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get a Plan")]
        public async Task<IActionResult> GetPlan(int planId)
        {
            var result = await _mediator.Send(new GetPlanByIdCommand(planId));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Create A Plan")]
        public async Task<IActionResult> CreatePlan([FromBody] AddPlanRequest plan)
        {
            var result = await _mediator.Send(new AddPlanCommand(plan));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("Update A Plan")]
        public async Task<IActionResult> UpdatePlan([FromBody] UpdatePlanRequest plan)
        {
            var result = await _mediator.Send(new UpdatePlanCommand(plan));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
        [HttpDelete("Delete a Plan")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePlan(int planId)
        {
            var result = await _mediator.Send(new DeletePlanCommand(planId));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
    }
}
