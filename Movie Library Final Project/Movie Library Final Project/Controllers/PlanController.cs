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
            return Ok(await _mediator.Send(new GetAllPlansCommand()));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Get a Plan")]
        public async Task<IActionResult> GetPlan(int planId)
        {
            var result = await _mediator.Send(new GetPlanByIdCommand(planId));
            if (result == null) return NotFound("Plan Doesn't Exist");
            return Ok(await _mediator.Send(new GetPlanByIdCommand(planId)));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Create A Plan")]
        public async Task<IActionResult> CreatePlan([FromBody] AddPlanRequest plan)
        {
            return Ok(await _mediator.Send(new AddPlanCommand(plan)));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("Update A Plan")]
        public async Task<IActionResult> UpdatePlan([FromBody] UpdatePlanRequest plan)
        {
            var result = await _mediator.Send(new GetPlanByIdCommand(plan.PlanId));
            if (result == null) return NotFound("Plan Doesn't Exist");
            return Ok(await _mediator.Send(new UpdatePlanCommand(plan)));
        }
        [HttpDelete("Delete a Plan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePlan(int planId)
        {
            var result = await _mediator.Send(new GetPlanByIdCommand(planId));
            if (result == null) return NotFound("Plan Doesn't Exist");
            return Ok(await _mediator.Send(new DeletePlanCommand(planId)));
        }
    }
}
